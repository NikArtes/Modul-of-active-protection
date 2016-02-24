using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Core.Extentions;

namespace Core
{
    public static class XmlLoggerManager
    {
        private static readonly Regex _regex = new Regex(@"\[[0-9]*:[0-9]*\]");

        public static void MakeXml(string[] paths, string pathToFile)
        {
            XDocument doc = new XDocument().LoadOrCreate(pathToFile);
            foreach (var path in paths)
            {
                var replacedPath = _regex.Replace(path, string.Empty);
                var vlogennost = string.Concat("levl", replacedPath.Split('\\').Length);
                if (doc.Root.Element(vlogennost) == null)
                {
                    doc.Root.Add(new XElement(vlogennost, new XElement("Add", replacedPath)));
                }
                else
                {
                    if (doc.Root.Element(vlogennost).Elements().Any() && !doc.Root.Element(vlogennost).Elements().Select(x => x.Value).Contains(replacedPath))
                    {
                        doc.Root.Element(vlogennost).Add(new XElement("Add", replacedPath));
                    }
                }
            }

            doc.Save(pathToFile);
        }

        public static IEnumerable<string> GetXml(string pathToFile, string level)
        {
            var result = new List<string>();

            using (var streamFile = new FileStream(pathToFile, FileMode.Open))
            {
                var xDocument = XDocument.Load(streamFile);
                if (xDocument.Root != null && xDocument.Root.Element(level) != null)
                {
                    var xElements = xDocument.Root.Element(level).Elements("Add");
                    if (xElements.Any())
                    {
                        result = xElements.Select(x => x.Value.ToString()).ToList();
                    }
                }
            }
            return result;
        }

        public static bool CheckPathInXml(string pathToFile, string path)
        {
            var replacedPath = _regex.Replace(path, string.Empty);

            var enumerable = GetXml(pathToFile, string.Concat("levl", replacedPath.Split('\\').Length));

            return enumerable.Contains(replacedPath);
        }
    }
}