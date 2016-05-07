using System;

namespace LibraryInjected.FunctionBehaviors
{
    public abstract class FunctionBehavior : MarshalByRefObject
    {
        public abstract void Action(string path);
    }
}