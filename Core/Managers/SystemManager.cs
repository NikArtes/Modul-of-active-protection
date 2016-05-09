using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Core.Dtos;

namespace Core.Managers
{
    public static class SystemManager
    {
        private static readonly Dictionary<string, AppDomain> ChildDomains = new Dictionary<string, AppDomain>();

        public static void CreateNewInjectProcess(SystemState state, ProcessDto processDto)
        {
            if (ChildDomains.ContainsKey(processDto.ProcName))
            {
                AppDomain.Unload(ChildDomains[processDto.ProcName]);
                ChildDomains.Remove(processDto.ProcName);
            }
            ChildDomains.Add(processDto.ProcName, AppDomain.CreateDomain("hookDomain"));

            ChildDomains[processDto.ProcName].CreateInstanceFromAndUnwrap(AppDomain.CurrentDomain.BaseDirectory + "InterceptedModule.dll", "InterceptedModule.InterseptDll", false, BindingFlags.CreateInstance, null, new[] { (object)state, (object)processDto }, CultureInfo.InvariantCulture, null);
        }
    }
}