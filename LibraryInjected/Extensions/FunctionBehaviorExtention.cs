using System;
using System.Linq;
using System.Reflection;
using Core;
using LibraryInjected.FunctionBehaviors;
using LibraryInjected.FunctionsInjected;
using LoggerModule;

namespace LibraryInjected.Extensions
{
    public static class FunctionBehaviorExtention
    {
        public static FunctionInjected CreateAttachedType(this FunctionBehavior functionBehavior)
        {
            FunctionInjected result = null;

            var attributes = functionBehavior.GetType().GetCustomAttributes<AttachedTypeAttribute>(false).ToArray();

            if (attributes.Any())
            {
                result = (FunctionInjected)Activator.CreateInstance(attributes.First().TypeMustCreate, functionBehavior);
            }

            return result;
        }
    }
}