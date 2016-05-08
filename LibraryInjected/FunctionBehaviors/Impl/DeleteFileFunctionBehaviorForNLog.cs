﻿using Core;
using Core.Managers;
using LibraryInjected.FunctionsInjected.Impl;

namespace LibraryInjected.FunctionBehaviors.Impl
{
    [AttachedType(typeof(DeleteFileFunctionInjected))]
    public class DeleteFileFunctionBehaviorForNLog : FunctionBehaviorForNLog
    {
        public override void Action(string path, string procName)
        {
            if (!XmlLoggerManager.CheckPathInXml(AppConfigManager.GetPathToXmlForProcess(procName), path.Trim('\\')))
            {
                Logger.Info(string.Concat("DeleteFile function call in ", path), procName);
            }
        }
    }
}