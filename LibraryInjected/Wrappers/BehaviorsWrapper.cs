using System;
using System.Linq;
using System.Reflection;
using LibraryInjected.FunctionBehaviors;

namespace LibraryInjected.Wrappers
{
    public class BehaviorsWrapper<T> : Wrapper where T : FunctionBehavior 
    {
        public BehaviorsWrapper()
        {
            Functions = Assembly.GetAssembly(typeof(FunctionBehavior)).GetTypes()
                .Where(x => x.IsSubclassOf(typeof(T)))
                .Select(type => (FunctionBehavior)Activator.CreateInstance(type))
                .ToList();
        }
    }
}