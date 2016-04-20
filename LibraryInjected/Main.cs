using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using EasyHook;
using LoggerModule;

namespace LibraryInjected
{
    using Core;

    public class Main : IEntryPoint
    {
        private Stack<Tuple<string, string>> Queue = new Stack<Tuple<string, string>>();
        private IFileMon Interface;
        private LocalHook CreateFileHook;
        private LocalHook DeleteFileWHook;
        private LocalHook DeleteFileAHook;

        public Main(RemoteHooking.IContext InContext, string InChannelName)
        {
            if (Class1.State == SystemState.Scanning)
            {
                this.Interface = RemoteHooking.IpcConnectClient<FileMonForXml>(InChannelName);
            }
            else
            {
                this.Interface = RemoteHooking.IpcConnectClient<FileMonForNLog>(InChannelName);
            }
        }

        public void Run(RemoteHooking.IContext InContext, string InChannelName)
        {
            try
            {
                this.CreateFileHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "CreateFileW"), (Delegate)new Main.DCreateFile(Main.CreateFile_Hooked), (object)this);

                this.DeleteFileWHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "DeleteFileW"), (Delegate)new Main.DDeleteFileW(Main.DeleteFileW_Hooked), (object)this);

                this.DeleteFileAHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "DeleteFileA"), (Delegate)new Main.DDeleteFileA(Main.DeleteFileA_Hooked), (object)this);

                this.CreateFileHook.ThreadACL.SetExclusiveACL(new int[1]);

                this.DeleteFileWHook.ThreadACL.SetExclusiveACL(new int[1]);

                this.DeleteFileAHook.ThreadACL.SetExclusiveACL(new int[1]);
            }
            catch (Exception ex)
            {
                this.Interface.ReportException(ex);
                return;
            }
            this.Interface.IsInstalled(RemoteHooking.GetCurrentProcessId());
            RemoteHooking.WakeUpProcess();
            try
            {
                while (true)
                {
                    Thread.Sleep(500);
                    if (this.Queue.Count > 0)
                    {
                        string[] InFileNamesForCreate = null;
                        string[] InFileNamesForDelete = null;
                        lock (this.Queue)
                        {
                            InFileNamesForCreate = this.Queue.Where(x => x.Item1 == "CreateFile").Select(x => x.Item2).ToArray();
                            InFileNamesForDelete = this.Queue.Where(x => x.Item1 == "DeleteFile").Select(x => x.Item2).ToArray();
                            this.Queue.Clear();
                        }
                        this.Interface.OnCreateFile(RemoteHooking.GetCurrentProcessId(), InFileNamesForCreate);
                        this.Interface.OnDeleteFile(RemoteHooking.GetCurrentProcessId(), InFileNamesForDelete);
                    }
                }
            }
            catch(Exception ex)
            {
                Interface.ReportException(ex);
            }
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
                Main main = (Main)HookRuntimeInfo.Callback;
                lock (main.Queue)
                  main.Queue.Push(new Tuple<string, string>("CreateFile", InFileName));
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return Main.CreateFile(InFileName, InDesiredAccess, InShareMode, InSecurityAttributes, InCreationDisposition, InFlagsAndAttributes, InTemplateFile);
        }

        private static bool DeleteFileW_Hooked(string InFileName)
        {
            try
            {
                Main main = (Main)HookRuntimeInfo.Callback;
                lock (main.Queue)
                  main.Queue.Push(new Tuple<string, string>("DeleteFile", InFileName));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return Main.DeleteFileW(InFileName);
        }

        private static bool DeleteFileA_Hooked(string InFileName)
        {
            try
            {
                Main main = (Main)HookRuntimeInfo.Callback;
                lock (main.Queue)
                  main.Queue.Push(new Tuple<string, string>("DeleteFile", InFileName));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return Main.DeleteFileA(InFileName);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate IntPtr DCreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate bool DDeleteFileW(string InFileName);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate bool DDeleteFileA(string InFileName);
    }

}
