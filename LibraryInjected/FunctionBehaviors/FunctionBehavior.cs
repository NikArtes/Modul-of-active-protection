using System;

namespace LibraryInjected.FunctionBehaviors
{
    [Serializable]
    public abstract class FunctionBehavior 
    {
        public abstract void Action(string path);
    }
}