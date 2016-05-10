using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using System.Reflection;
using System.Text.RegularExpressions;
using Core.Dtos;
using NLog.LayoutRenderers;

namespace Core.Managers
{
    public static class SystemManager
    {
        private static readonly Dictionary<string, AppDomain> ChildDomains;

        private static ManagementEventWatcher _processCloseEventWatcher;

        static SystemManager()
        {
            ChildDomains = new Dictionary<string, AppDomain>();

            _processCloseEventWatcher = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");

            _processCloseEventWatcher.EventArrived += (sender, eventArgs) =>
                                                          {
                                                              var processName = eventArgs.NewEvent.Properties["ProcessName"].Value.ToString().Replace(".exe", string.Empty);
                                                              if (ChildDomains.ContainsKey(processName))
                                                              {
                                                                  AppDomain.Unload(ChildDomains[processName]);
                                                                  ChildDomains.Remove(processName);
                                                                  Logger.Info($"process {processName} is closing and his domain was unloaded", "common");
                                                              }
                                                          };

            _processCloseEventWatcher.Start();
        }

        public static void CreateNewInjectProcess(SystemState state, ProcessDto processDto)
        {
            if (ChildDomains.ContainsKey(processDto.ProcName))
            {
                AppDomain.Unload(ChildDomains[processDto.ProcName]);
                ChildDomains.Remove(processDto.ProcName);
            }
            ChildDomains.Add(processDto.ProcName, AppDomain.CreateDomain(processDto.ProcName));

            ChildDomains[processDto.ProcName].CreateInstanceFromAndUnwrap(AppDomain.CurrentDomain.BaseDirectory + "InterceptedModule.dll", "InterceptedModule.InterseptDll", false, BindingFlags.CreateInstance, null, new[] {(object) state, (object) processDto}, CultureInfo.InvariantCulture, null);
        }
    }
}