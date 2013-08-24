using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CompanyName.Utilities.Xml
{
    public static class XmlStream
    {
        public static IEnumerable<XElement> StreamElements(string fileName, string elementName)
        {
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == elementName))
                    {
                        XElement e = XElement.ReadFrom(reader) as XElement;
                        yield return e;
                    }
                }
                reader.Close();
            }
        }
    }
}
