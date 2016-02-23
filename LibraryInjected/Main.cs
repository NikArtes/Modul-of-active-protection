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
    public class Main : IEntryPoint
    {
        private Stack<string> Queue = new Stack<string>();
        private IFileMon Interface;
        private LocalHook CreateFileHook;

        public Main(RemoteHooking.IContext InContext, string InChannelName)
        {
            this.Interface = RemoteHooking.IpcConnectClient<FileMonForNLog>(InChannelName);
        }
        //TODO подумать как распаралелить 
        public void Run(RemoteHooking.IContext InContext, string InChannelName)
        {
            try
            {
                this.CreateFileHook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "CreateFileW"), (Delegate)new Main.DCreateFile(Main.CreateFile_Hooked), (object)this);
                this.CreateFileHook.ThreadACL.SetExclusiveACL(new int[1]);
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
                        string[] InFileNames = (string[])null;
                        lock (this.Queue)
                        {
                            InFileNames = this.Queue.ToArray();
                            this.Queue.Clear();
                        }
                        this.Interface.OnCreateFile(RemoteHooking.GetCurrentProcessId(), InFileNames);
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

        private static IntPtr CreateFile_Hooked(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile)
        {
            try
            {
                Main main = (Main)HookRuntimeInfo.Callback;
                lock (main.Queue)
                  main.Queue.Push("[" + RemoteHooking.GetCurrentProcessId().ToString() + ":" + RemoteHooking.GetCurrentThreadId().ToString() + "]: \"" + InFileName + "\"");
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return Main.CreateFile(InFileName, InDesiredAccess, InShareMode, InSecurityAttributes, InCreationDisposition, InFlagsAndAttributes, InTemplateFile);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate IntPtr DCreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);
    }

}
