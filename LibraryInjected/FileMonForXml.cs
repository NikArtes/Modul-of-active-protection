using System;
using System.Linq;
using Core;
using LoggerModule;

namespace LibraryInjected
{
    public class FileMonForXml : MarshalByRefObject, IFileMon
    {
        public void IsInstalled(int inClientPid)
        {
            Logger.Info($"FileMon has been installed in target {inClientPid}.\r\n");
        }

        public void OnCreateFile(int inClientPid, string[] inFileNames)
        {
            XmlLoggerManager.MakeXml(inFileNames.Select(x => x.Trim('\\')).ToArray(), "C:\\logs\\LoggerModule\\test.xml" );
        }

        //TODO подумать что тут делать с xml
        public void OnDeleteFile(int inClientPid, string[] inFileNames)
        {
        }

        public void ReportException(Exception inInfo)
        {
            Logger.Error(string.Format("The target process has reported an error:\r\n" + inInfo.ToString()));
        }
    }
}