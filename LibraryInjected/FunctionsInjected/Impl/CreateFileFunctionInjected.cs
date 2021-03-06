﻿using System;
using System.Runtime.InteropServices;
using Core.Dtos;
using Core.Managers;
using EasyHook;
using LibraryInjected.FunctionBehaviors;

namespace LibraryInjected.FunctionsInjected.Impl
{
    public class CreateFileFunctionInjected : FunctionInjected
    {
        private static FunctionBehavior _behavior;

        private static ProcessDto _processDto;

        public CreateFileFunctionInjected(FunctionBehavior behavior, ProcessDto processDto)
        {
            _behavior = behavior;

            _processDto = processDto;

            _hook = LocalHook.Create(LocalHook.GetProcAddress("kernel32.dll", "CreateFileW"), new DCreateFile(CreateFile_Hooked), this);

            _hook.ThreadACL.SetExclusiveACL(new int[0]);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr CreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);

        private static IntPtr CreateFile_Hooked(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile)
        {
            try
            {
                _behavior.Action(InFileName, _processDto.ProcName);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, _processDto.ProcName);
            }
            return CreateFile(InFileName, InDesiredAccess, InShareMode, InSecurityAttributes, InCreationDisposition, InFlagsAndAttributes, InTemplateFile);
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate IntPtr DCreateFile(string InFileName, uint InDesiredAccess, uint InShareMode, IntPtr InSecurityAttributes, uint InCreationDisposition, uint InFlagsAndAttributes, IntPtr InTemplateFile);
    }
}