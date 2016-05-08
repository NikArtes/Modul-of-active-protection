using System;
using System.Runtime.InteropServices;
using Core.Dtos;
using Core.Managers;
using EasyHook;
using LibraryInjected.FunctionBehaviors;

namespace LibraryInjected.FunctionsInjected.Impl
{
    public class DeleteFileFunctionInjected : FunctionInjected
    {
        private static FunctionBehavior _behavior;

        private static ProcessDto _processDto;

        public DeleteFileFunctionInjected(FunctionBehavior behavior, ProcessDto processDto)
        {
            _behavior = behavior;

            _processDto = processDto;

            _hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "DeleteFileW"), new DDeleteFile(DeleteFile_Hooked), this);

            _hook.ThreadACL.SetExclusiveACL(new int[0]);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern bool DeleteFile(string InFileName);

        private static bool DeleteFile_Hooked(string InFileName)
        {
            try
            {
                _behavior.Action(InFileName, _processDto.ProcName);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, _processDto.ProcName);
            }
            return DeleteFile(InFileName);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate bool DDeleteFile(string InFileName);
    }
}