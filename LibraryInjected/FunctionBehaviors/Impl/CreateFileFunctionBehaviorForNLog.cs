using Core;
using Core.Managers;
using LibraryInjected.FunctionsInjected.Impl;

namespace LibraryInjected.FunctionBehaviors.Impl
{
    [AttachedType(typeof(CreateFileFunctionInjected))]
    public class CreateFileFunctionBehaviorForNLog : FunctionBehaviorForNLog
    {
        public CreateFileFunctionBehaviorForNLog()
        {
            _functionName = "CreateFile";
        }

        public override void Action(string path, string procName)
        {
            if (!XmlLoggerManager.CheckPathInXml(AppConfigManager.GetPathToXmlForProcess(procName, _functionName), path.Trim('\\')))
            {
                Logger.Info(string.Concat("CreateFile function call in ", path), procName);
            }
        }
    }
}