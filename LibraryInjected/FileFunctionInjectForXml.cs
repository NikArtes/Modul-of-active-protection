using System;
using System.Linq;
using Core;
using LoggerModule;

namespace LibraryInjected
{
    public class FileFunctionInjectForXml : MarshalByRefObject, IFileFunctionInject
    {
        public void OnCreateFile(string[] inFileNames)
        {
            XmlLoggerManager.MakeXml(inFileNames.Select(x => x.Trim('\\')).ToArray(), "C:\\logs\\LoggerModule\\test.xml" );
        }

        //TODO подумать что тут делать с xml
        public void OnDeleteFile(string[] inFileNames)
        {
        }
    }
}