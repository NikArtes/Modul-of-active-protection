using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using LoggerModule;

namespace LibraryInjected
{
    public class FileMonForNLog : MarshalByRefObject, IFileMon
    {
        public void IsInstalled(int inClientPid)
        {
            Logger.Info($"FileMon has been installed in target {(object) inClientPid}.\r\n");
        }

        public void OnCreateFile(int inClientPid, string[] inFileNames)
        {
            foreach (string fileNames in inFileNames)
            {
                if (!XmlLoggerManager.CheckPathInXml("C:\\logs\\LoggerModule\\test.xml", fileNames.Trim('\\')))
                {
                    Logger.Info(fileNames);
                }
            }
        }

        public void ReportException(Exception inInfo)
        {
            Logger.Error(string.Format("The target process has reported an error:\r\n" + inInfo.ToString()));
        }

        public void Ping()
        {
            //Logger.Info(string.Concat("Ping {0}", DateTime.Now));
        }
    }
}