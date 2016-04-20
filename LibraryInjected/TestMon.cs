namespace LibraryInjected
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Core;
    using EasyHook;
    using LoggerModule;

    public class TestMon
    {
        protected Stack<Tuple<string, string>> Queue = new Stack<Tuple<string, string>>();
        private IFileMon Interface;
        private LocalHook CreateFileHook;
        private LocalHook DeleteFileWHook;
        private LocalHook DeleteFileAHook;

        public void Initialize(string InChannelName) 
        {
            if (Class1.State == SystemState.Scanning)
            {
                Interface = RemoteHooking.IpcConnectClient<FileMonForXml>(InChannelName);
            }
            else
            {
                Interface = RemoteHooking.IpcConnectClient<FileMonForNLog>(InChannelName);
            }

            CreateFileHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "CreateFileW"), new DCreateFile(CreateFile_Hooked), this);

            DeleteFileWHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "DeleteFileW"), new DDeleteFileW(DeleteFileW_Hooked), this);

            DeleteFileAHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "DeleteFileA"), new DDeleteFileA(DeleteFileA_Hooked), this);

            CreateFileHook.ThreadACL.SetExclusiveACL(new int[1]);

            DeleteFileWHook.ThreadACL.SetExclusiveACL(new int[1]);

            DeleteFileAHook.ThreadACL.SetExclusiveACL(new int[1]);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr CreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern bool DeleteFileW(string InFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern bool DeleteFileA(string InFileName);

        private static IntPtr CreateFile_Hooked(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile)
        {
            try
            {
                var main = (TestMon)HookRuntimeInfo.Callback;
                main.Interface.OnCreateFile(new[] { InFileName });

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return CreateFile(InFileName, InDesiredAccess, InShareMode, InSecurityAttributes, InCreationDisposition, InFlagsAndAttributes, InTemplateFile);
        }

        private static bool DeleteFileW_Hooked(string InFileName)
        {
            try
            {
                var main = (TestMon)HookRuntimeInfo.Callback;
                main.Interface.OnDeleteFile(new []{ InFileName });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return DeleteFileW(InFileName);
        }

        private static bool DeleteFileA_Hooked(string InFileName)
        {
            try
            {
                var main = (TestMon)HookRuntimeInfo.Callback;
                main.Interface.OnDeleteFile(new[] { InFileName });

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return DeleteFileA(InFileName);
        }


        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate IntPtr DCreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate bool DDeleteFileW(string InFileName);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate bool DDeleteFileA(string InFileName);
    }
}