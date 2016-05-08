using System.Configuration;

namespace Core.Managers
{
    public static class AppConfigManager
    {
        public static string GetBasePathToLoggerModule()
        {
            return ConfigurationManager.AppSettings["basePathToLoggerModule"];
        }

        public static string GetNameOfXmlFile()
        {
            return ConfigurationManager.AppSettings["nameOfXMLFile"];
        }

        public static string GetNameOfLogFile()
        {
            return ConfigurationManager.AppSettings["nameOfLogFile"];
        }

        public static string GetNameOfErrorFile()
        {
            return ConfigurationManager.AppSettings["nameOfErrorFile"];
        }

        public static string GetPathToLogForProcess(string processName = "common")
        {
            return string.Concat(GetBasePathToLoggerModule(),
                                    processName,
                                    "\\",
                                    GetNameOfLogFile());
        }

        public static string GetPathToErrorForProcess(string processName = "common")
        {
            return string.Concat(GetBasePathToLoggerModule(),
                                 processName,
                                 "\\",
                                 GetNameOfErrorFile());
        }

        public static string GetPathToXmlForProcess(string processName = "common")
        {
            return string.Concat(GetBasePathToLoggerModule(),
                                 processName,
                                 "\\",
                                 GetNameOfXmlFile());
        }
    }
}