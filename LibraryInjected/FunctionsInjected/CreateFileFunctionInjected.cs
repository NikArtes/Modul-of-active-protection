using System;
using System.Runtime.InteropServices;
using EasyHook;
using LibraryInjected.FunctionBehaviors;
using LoggerModule;

namespace LibraryInjected.FunctionsInjected
{
    public class CreateFileFunctionInjected : FunctionInjected
    {
        private FunctionBehavior _behavior;

        public CreateFileFunctionInjected(FunctionBehavior behavior)
        {
            _behavior = behavior;

            _hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "CreateFileW"), new DCreateFile(CreateFile_Hooked), this);

            _hook.ThreadACL.SetExclusiveACL(new int[1]);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr CreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);

        private static IntPtr CreateFile_Hooked(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile)
        {
            try
            {
                var main = (CreateFileFunctionInjected)HookRuntimeInfo.Callback;
                main._behavior.Action(InFileName);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return CreateFile(InFileName, InDesiredAccess, InShareMode, InSecurityAttributes, InCreationDisposition, InFlagsAndAttributes, InTemplateFile);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate IntPtr DCreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);
    }
}