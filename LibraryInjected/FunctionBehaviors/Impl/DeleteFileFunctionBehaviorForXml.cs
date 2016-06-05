using Core;
using Core.Managers;
using LibraryInjected.FunctionsInjected.Impl;

namespace LibraryInjected.FunctionBehaviors.Impl
{
    [AttachedType(typeof(DeleteFileFunctionInjected))]
    public class DeleteFileFunctionBehaviorForXml : FunctionBehaviorForXml
    {
        public DeleteFileFunctionBehaviorForXml()
        {
            _functionName = "DeleteFile";
        }

        public override void Action(string keyForWhiteList, string procName)
        {
            XmlLoggerManager.MakeXml(keyForWhiteList.Trim('\\'), AppConfigManager.GetPathToXmlForProcess(procName, _functionName));
        }
    }
}