using System;
using Core;
using LibraryInjected.FunctionsInjected;
using LoggerModule;

namespace LibraryInjected.FunctionBehaviors
{
    [Serializable]
    [AttachedType(typeof(CreateFileFunctionInjected), SystemState.Locking)]
    public class CreateFailFunctionBehaviorForNLog : FunctionBehavior
    {
        public override void Action(string path)
        {
            if (!XmlLoggerManager.CheckPathInXml("C:\\logs\\LoggerModule\\test.xml", path.Trim('\\')))
            {
                Logger.Info(string.Concat("CreateFile function call in ", path));
            }
        }
    }
}