using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using EasyHook;
using LibraryInjected;
using LoggerModule;
using System.Security.Principal;

namespace InterceptedModule
{
    public static class InterseptDll
    {
        private static string ChannelName;

        public static void Main(string[] args)
        {
            int InTargetPID = 0;
            string InEXEPath = (string)null;
            foreach (var process in Process.GetProcessesByName("ccleaner64"))
            {
                InTargetPID = process.Id;
                break;
            }

            if (InTargetPID == -1)
            {
                Logger.Error("No process exists with that name!");
                return;
            }
            Intersept(InTargetPID, InEXEPath);
        }

        private static void Intersept(int inTargetPID, string inEXEPath)
        {
            try
            {
                RemoteHooking.IpcCreateServer<FileMonForNLog>(ref ChannelName, WellKnownObjectMode.SingleCall, WellKnownSidType.WorldSid);
                string str = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LibraryInjected.dll");
                if (string.IsNullOrEmpty(inEXEPath))
                {
                    RemoteHooking.Inject(inTargetPID, str, str, (object)ChannelName);
                    Logger.Info($"Injected to process {inTargetPID}");
                }
                else
                {
                    RemoteHooking.CreateAndInject(inEXEPath, "", 0, InjectionOptions.DoNotRequireStrongName, str, str, out inTargetPID, (object)ChannelName);
                    Logger.Info($"Created and injected process {(object) inTargetPID}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"There was an error while connecting to target:\r\n{(object) ex.ToString()}");
            }
        }

    }
}
