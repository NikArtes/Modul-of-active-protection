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
using Core.Dtos;
using Core.Managers;

namespace InterceptedModule
{
    [Serializable]
    public class InterseptDll
    {
        private string ChannelName;

        public void Main(SystemState state, ProcessDto processDto)
        {
            if (processDto.ProcId == -1)
            {
                Logger.Error("No process exists with that name!", processDto.ProcName);
                return;
            }
            Intersept(processDto, state);
        }

        private void Intersept(ProcessDto processDto, SystemState state)
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

                RemoteHooking.Inject(processDto.ProcId, str, str, (object)processDto, (object)state, (object)ChannelName);
                Logger.Info($"Injected to process {processDto.ProcId}", processDto.ProcName);

            }
            catch (Exception ex)
            {
                Logger.Error($"There was an error while connecting to target:\r\n{(object) ex.ToString()}", processDto.ProcName);
            }
        }

    }
}
