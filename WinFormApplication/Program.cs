using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using Core.Dtos;
using Core.Managers;
using InterceptedModule;

namespace WinFormApplication
{
    static class Program
    {
        private static Dictionary<string, AppDomain> ChildDomains { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ChildDomains = new Dictionary<string, AppDomain>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form());
        }

        internal static void CreateNewInjectProcess(SystemState state, ProcessDto processDto)
        {
            Logger.Info(ChildDomains.Count.ToString(), "common");
            if (ChildDomains.ContainsKey(processDto.ProcName))
            {
                AppDomain.Unload(ChildDomains[processDto.ProcName]);
                ChildDomains.Remove(processDto.ProcName);
            }
            ChildDomains.Add(processDto.ProcName, AppDomain.CreateDomain("hookDomain"));

            var unwrap = (InterseptDll)ChildDomains[processDto.ProcName].CreateInstanceFromAndUnwrap(AppDomain.CurrentDomain.BaseDirectory + "InterceptedModule.dll", "InterceptedModule.InterseptDll");
            unwrap.Main(state, processDto);
        }
    }
}
