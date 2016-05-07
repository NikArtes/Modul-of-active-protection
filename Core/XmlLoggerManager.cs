using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Core.Extentions;

namespace Core
{
    public static class XmlLoggerManager
    {
        public static void MakeXml(string path, string pathToFile)
        {
            XDocument doc = new XDocument().LoadOrCreate(pathToFile);

            var vlogennost = string.Concat("levl", path.Split('\\').Length);
            if (doc.Root.Element(vlogennost) == null)
            {
                doc.Root.Add(new XElement(vlogennost, new XElement("Add", path)));
            }
            else
            {
                if (doc.Root.Element(vlogennost).Elements().Any() && !doc.Root.Element(vlogennost).Elements().Select(x => x.Value).Contains(path))
                {
                    doc.Root.Element(vlogennost).Add(new XElement("Add", path));
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
            var enumerable = GetXml(pathToFile, string.Concat("levl", path.Split('\\').Length));

            return enumerable.Contains(path);
        }
    }
}