using System.Configuration;

namespace Core.Managers
{
    public static class AppConfigManager
    {
        public static string GetPathToXmlFile()
        {
            return ConfigurationManager.AppSettings["pathToXMLFile"];
        }
    }
}