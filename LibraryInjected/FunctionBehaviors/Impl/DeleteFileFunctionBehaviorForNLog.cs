using Core;
using Core.Managers;
using LibraryInjected.FunctionsInjected.Impl;

namespace LibraryInjected.FunctionBehaviors.Impl
{
    [AttachedType(typeof(DeleteFileFunctionInjected))]
    public class DeleteFileFunctionBehaviorForNLog : FunctionBehaviorForNLog
    {
        public DeleteFileFunctionBehaviorForNLog()
        {
            _functionName = "DeleteFile";
        }

        public override void Action(string keyForWhiteList, string procName)
        {
            if (!XmlLoggerManager.CheckPathInXml(AppConfigManager.GetPathToXmlForProcess(procName, _functionName), keyForWhiteList.Trim('\\')))
            {
                Logger.Info(string.Concat("DeleteFile function call in ", keyForWhiteList), procName);
            }
        }
    }
}