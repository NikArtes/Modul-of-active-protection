using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using LoggerModule;

namespace LibraryInjected
{
    public class FileFunctionInjectForNLog : MarshalByRefObject, IFileFunctionInject
    {
        public void OnCreateFile(string[] inFileNames)
        {
            foreach (string fileNames in inFileNames)
            {
                if (!XmlLoggerManager.CheckPathInXml("C:\\logs\\LoggerModule\\test.xml", fileNames.Trim('\\')))
                {
                    Logger.Info(string.Concat("CreateFile function call in ",fileNames));
                }
            }
        }

        public void OnDeleteFile(string[] inFileNames)
        {
            foreach (var fileName in inFileNames)
            {
                if (!XmlLoggerManager.CheckPathInXml("C:\\logs\\LoggerModule\\test.xml", fileName.Trim('\\')))
                {
                    Logger.Info(string.Concat("DeleteFile function call in ", fileName));
                }
            }
        }
    }
}