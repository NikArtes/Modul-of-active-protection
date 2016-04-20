using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using EasyHook;
using LoggerModule;
using Core;
using System.Dynamic;

namespace LibraryInjected
{
    public class Main : IEntryPoint 
    {
        private readonly TestMon _testMon;

        public Main(RemoteHooking.IContext InContext, string InChannelName)
        {
            _testMon = new TestMon();
        }

        public void Run(RemoteHooking.IContext InContext, string InChannelName)
        {
            RemoteHooking.WakeUpProcess();

            try
            {
                _testMon.Initialize(InChannelName);
                while (true)
                {

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
