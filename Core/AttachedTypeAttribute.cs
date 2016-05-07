using System;

namespace Core
{
    public class AttachedTypeAttribute : Attribute 
    {
        public Type TypeMustCreate { get; }

        public AttachedTypeAttribute(Type typeMustCreate)
        {
            TypeMustCreate = typeMustCreate;
        }
    }
}