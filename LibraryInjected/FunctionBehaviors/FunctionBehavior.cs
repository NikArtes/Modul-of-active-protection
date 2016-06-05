using System;

namespace LibraryInjected.FunctionBehaviors
{
    public abstract class FunctionBehavior : MarshalByRefObject
    {
        protected string _functionName;

        public abstract void Action(string path, string procName);
    }
}