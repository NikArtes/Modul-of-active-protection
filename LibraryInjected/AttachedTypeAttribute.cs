using System;
using Core;

namespace LibraryInjected
{
    public class AttachedTypeAttribute : Attribute
    {
        public Type TypeMustCreate { get; }

        public SystemState StateForCreate { get; }

        public AttachedTypeAttribute(Type typeMustCreate, SystemState stateForCreate) 
        {
            TypeMustCreate = typeMustCreate;
            StateForCreate = stateForCreate;
        }
    }
}