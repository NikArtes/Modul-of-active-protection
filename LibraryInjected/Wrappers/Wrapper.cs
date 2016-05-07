using System;
using System.Collections.Generic;
using LibraryInjected.FunctionBehaviors;

namespace LibraryInjected.Wrappers
{
    public abstract class Wrapper : MarshalByRefObject
    {
        public List<FunctionBehavior> Functions { get; protected set; }
    }
}