using Core;
using Core.Managers;
using LibraryInjected.FunctionsInjected.Impl;

namespace LibraryInjected.FunctionBehaviors.Impl
{
    [AttachedType(typeof(CreateFileFunctionInjected))]
    public class CreateFileFunctionBehaviorForXml : FunctionBehaviorForXml
    {
        public override void Action(string path, string procName)
        {
            XmlLoggerManager.MakeXml( path.Trim('\\'), AppConfigManager.GetPathToXmlForProcess(procName));
        }
    }
}