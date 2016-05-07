using Core;
using LibraryInjected.FunctionsInjected;

namespace LibraryInjected.FunctionBehaviors
{
    [AttachedType(typeof(CreateFileFunctionInjected))]
    public class CreateFailFunctionBehaviorForXml : FunctionBehaviorForXml
    {
        public override void Action(string path)
        {
            XmlLoggerManager.MakeXml( path.Trim('\\'), "C:\\logs\\LoggerModule\\test.xml");
        }
    }
}