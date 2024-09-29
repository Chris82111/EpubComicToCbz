using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Chris82111.ComicInfo.Model
{
    /// <summary>
    /// <see href="https://anansi-project.github.io/docs/comicinfo/schemas/v2.1"/>
    /// </summary>
    public abstract class ComicInfoBase
    {
        public static string NewLine { get; set; } = Environment.NewLine;
        public string ToXmlString(XmlWriterSettings settings)
        {
            return ToXmlString(this, settings);
        }

        public string ToXmlString()
        {
            return ToXmlString(this);
        }

        public static string ToXmlString(ComicInfoBase obj, XmlWriterSettings settings)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            var xml = "";

            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
                {
                    xmlSerializer.Serialize(new XmlWriterEE(writer), o: obj);
                    //xmlSerializer.Serialize(writer, o: obj);
                    xml = stringWriter.ToString();
                }
            }

            return xml;
        }

        public static string ToXmlString(ComicInfoBase obj)
        {
            var settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8,
                NewLineChars = NewLine,
                Indent = true,
                IndentChars = "    ",
            };

            return ToXmlString(obj, settings);
        }

        public static void Create(ComicInfoBase obj, string outputFileName)
        {
            using (XmlWriter writer = XmlWriter.Create(outputFileName))
            {
                writer.WriteRaw(NewLine);
                writer.WriteRaw(obj.ToXmlString());
                writer.Flush();
            }
        }
    }
}
