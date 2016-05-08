using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using Core.Dtos;
using InterceptedModule;

namespace WinFormApplication
{
    static class Program
    {
        private static AppDomain ChildDomain { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form());
        }

        internal static void CreateNewInjectProcess(SystemState state, ProcessDto processDto)
        {
            if (ChildDomain != null)
            {
                AppDomain.Unload(ChildDomain);
            }
            ChildDomain = AppDomain.CreateDomain("hookDomain");

            var unwrap = (InterseptDll)ChildDomain.CreateInstanceFromAndUnwrap(AppDomain.CurrentDomain.BaseDirectory + "InterceptedModule.dll", "InterceptedModule.InterseptDll");
            unwrap.Main(state, processDto);
        }
    }
}
