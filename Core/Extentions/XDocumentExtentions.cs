using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Core.Extentions
{
    public static class XDocumentExtentions
    {
        public static XDocument LoadOrCreate(this XDocument xDocument, string pathToFile)
        {
            if (!File.Exists(pathToFile))
            {
                using (var xmlTextWriter = new XmlTextWriter(pathToFile, Encoding.UTF8))
                {
                    xmlTextWriter.WriteStartElement("root");
                    xmlTextWriter.WriteEndElement();
                }
            }

            xDocument = XDocument.Load(pathToFile);

            return xDocument;
        }
    }
}