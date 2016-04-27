using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;
using LibraryInjected.Extensions;
using LibraryInjected.FunctionBehaviors;
using LibraryInjected.FunctionsInjected;
using LoggerModule;

namespace LibraryInjected
{
    public class BehaviorsWrapper : MarshalByRefObject
    {
        public SystemState State { get; }

        public List<FunctionBehavior> Functions { get; private set; }

        public BehaviorsWrapper(SystemState state)
        {
            State = state;
            Initialaize();
        }

        public BehaviorsWrapper()
        {
            Initialaize();
        }

        private void Initialaize()
        {
            try
            {
                Functions = new List<FunctionBehavior>();

                foreach (var type in Assembly.GetAssembly(typeof(FunctionBehavior)).GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(FunctionBehavior)) && x.GetCustomAttributes<AttachedTypeAttribute>(false).Any(a => a.StateForCreate == State)))
                {
                    Functions.Add((FunctionBehavior)Activator.CreateInstance(type));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}