using Core;
using Core.Managers;
using LibraryInjected.FunctionsInjected;

namespace LibraryInjected.FunctionBehaviors
{
    [AttachedType(typeof(CreateFileFunctionInjected))]
    public class CreateFailFunctionBehaviorForNLog : FunctionBehaviorForNLog
    {
        public override void Action(string path)
        {
            if (!XmlLoggerManager.CheckPathInXml(AppConfigManager.GetPathToXmlFile(), path.Trim('\\')))
            {
                Logger.Info(string.Concat("CreateFile function call in ", path));
            }
        }
    }
}