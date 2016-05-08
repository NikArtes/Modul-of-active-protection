using System;
using System.Collections.Generic;
using EasyHook;
using Core;
using Core.Dtos;
using Core.Managers;
using LibraryInjected.Extentions;
using LibraryInjected.FunctionBehaviors;
using LibraryInjected.FunctionsInjected;
using LibraryInjected.Wrappers;

namespace LibraryInjected
{
    public class Main : IEntryPoint
    {
        private readonly List<FunctionInjected> _functions;

        private readonly Wrapper _behaviorWrapper;

        public Main(RemoteHooking.IContext InContext, ProcessDto procDto, SystemState state, string InChannelName)
        {
            try
            {
                _functions = new List<FunctionInjected>();

                switch (state)
                {
                    case SystemState.Scanning:
                        _behaviorWrapper = RemoteHooking.IpcConnectClient<BehaviorsWrapper<FunctionBehaviorForXml>>(InChannelName);
                        break;
                    case SystemState.Locking:
                        _behaviorWrapper = RemoteHooking.IpcConnectClient<BehaviorsWrapper<FunctionBehaviorForNLog>>(InChannelName);
                        break;
                }

                foreach (var functionBehavior in _behaviorWrapper.Functions)
                {
                    _functions.Add(functionBehavior.CreateAttachedTypeOfFunctionInjected(procDto));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, procDto.ProcName);
            }
        }

        public void Run(RemoteHooking.IContext InContext, ProcessDto procDto, SystemState state, string InChannelName)
        {
            try
            {                
                RemoteHooking.WakeUpProcess();

                while (true)
                {
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, procDto.ProcName);
            }
        }
    }
}
