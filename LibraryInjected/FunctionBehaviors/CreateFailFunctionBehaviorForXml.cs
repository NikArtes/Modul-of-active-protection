using Core;
using Core.Managers;
using LibraryInjected.FunctionsInjected;

namespace LibraryInjected.FunctionBehaviors
{
    [AttachedType(typeof(CreateFileFunctionInjected))]
    public class CreateFailFunctionBehaviorForXml : FunctionBehaviorForXml
    {
        public override void Action(string path, string procName)
        {
            XmlLoggerManager.MakeXml( path.Trim('\\'), string.Concat(AppConfigManager.GetBasePathToLoggerModule(), 
                procName, 
                "\\", 
                AppConfigManager.GetNameOfXmlFile()));
        }
    }
}