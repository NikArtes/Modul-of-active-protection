using System;
using System.Linq;
using System.Reflection;
using Core;
using LibraryInjected.FunctionBehaviors;
using LibraryInjected.FunctionsInjected;

namespace LibraryInjected.Extentions
{
    public static class FunctionBehaviorExtention
    {
        public static FunctionInjected CreateAttachedTypeOfFunctionInjected(this FunctionBehavior functionBehavior)
        {
            FunctionInjected result = null;

            var attributes = functionBehavior.GetType().GetCustomAttributes<AttachedTypeAttribute>(false).ToArray();

            if (attributes.Any(x => x.TypeMustCreate.IsSubclassOf(typeof (FunctionInjected))))
            {
                result = (FunctionInjected)Activator.CreateInstance(attributes.First().TypeMustCreate, functionBehavior);
            }

            return result;
        }
    }
}