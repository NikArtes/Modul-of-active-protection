using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using EasyHook;
using LoggerModule;
using Core;
using System.Dynamic;
using Core.Extentions;
using LibraryInjected.Extensions;
using LibraryInjected.FunctionBehaviors;
using LibraryInjected.FunctionsInjected;

namespace LibraryInjected
{
    public class Main : IEntryPoint
    {
        private readonly List<FunctionInjected> _functions; 

        public Main(RemoteHooking.IContext InContext, string InChannelName)
        {
            _functions = new List<FunctionInjected>();
            BehaviorsWrapper behaviorsWrapper = RemoteHooking.IpcConnectClient<BehaviorsWrapper>(InChannelName);

            foreach (var functionBehavior in behaviorsWrapper.Functions)
            {
                var functionInjected = functionBehavior.CreateAttachedType();
                if (functionInjected != null)
                {
                    _functions.Add(functionBehavior.CreateAttachedType());
                }
            }
            Logger.Info(_functions.Count().ToString());
            Logger.Info(behaviorsWrapper.State.ToString());
        }

        public void Run(RemoteHooking.IContext InContext, string InChannelName)
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
                Logger.Error(ex.Message);
            }
        }
    }
}
