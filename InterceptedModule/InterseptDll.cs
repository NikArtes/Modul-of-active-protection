using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using EasyHook;
using System.Security.Principal;
using LibraryInjected.FunctionBehaviors;
using LibraryInjected.Wrappers;
using Core;
using Core.Managers;

namespace InterceptedModule
{
    [Serializable]
    public class InterseptDll
    {
        private string ChannelName;

        public void Main(SystemState state)
        {
            var InTargetPID = 0;
            var InEXEPath = (string)null;
            foreach (var process in Process.GetProcessesByName("Notepad++"))
            {
                InTargetPID = process.Id;
                break;
            }

            if (InTargetPID == -1)
            {
                Logger.Error("No process exists with that name!");
                return;
            }
            Intersept(InTargetPID, InEXEPath, state);
        }

        private void Intersept(int inTargetPID, string inEXEPath, SystemState state)
        {
            try
            {
                switch (state)
                {
                    case SystemState.Scanning:
                        RemoteHooking.IpcCreateServer<BehaviorsWrapper<FunctionBehaviorForXml>>(ref ChannelName, WellKnownObjectMode.SingleCall, WellKnownSidType.WorldSid);
                        break;
                    case SystemState.Locking:
                        RemoteHooking.IpcCreateServer<BehaviorsWrapper<FunctionBehaviorForNLog>>(ref ChannelName, WellKnownObjectMode.SingleCall, WellKnownSidType.WorldSid);
                        break;
                }

                var str = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LibraryInjected.dll");
                if (string.IsNullOrEmpty(inEXEPath))
                {
                    RemoteHooking.Inject(inTargetPID, str, str, (object)state, (object)ChannelName);
                    Logger.Info($"Injected to process {inTargetPID}");
                }
                else
                {
                    RemoteHooking.CreateAndInject(inEXEPath, "", 0, InjectionOptions.DoNotRequireStrongName, str, str, out inTargetPID, (object)state, (object)ChannelName);
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
