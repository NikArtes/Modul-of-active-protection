using System;
using System.Linq;
using Core;
using LibraryInjected.FunctionsInjected;

namespace LibraryInjected.FunctionBehaviors
{
    [Serializable]
    [AttachedType(typeof(CreateFileFunctionInjected), SystemState.Scanning)]
    public class CreateFailFunctionBehaviorForXml : FunctionBehavior
    {
        public override void Action(string path)
        {
            XmlLoggerManager.MakeXml( path.Trim('\\'), "C:\\logs\\LoggerModule\\test.xml");
        }
    }
}