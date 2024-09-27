using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Chris82111.ComicInfo.Types
{
    /// <summary>
    /// Community rating of the book, from 0.0 to 5.0.
    /// </summary>
    public class CommunityRating : IXmlSerializable
    {
        public double? Value
        {
            private set { _Rating = value; }
            get { return _Rating; }
        }

        public static implicit operator double?(CommunityRating obj) => obj._Rating;
        public static implicit operator CommunityRating(double? rating) => new CommunityRating(rating);

        private double? _Rating;

        public CommunityRating()
        {
            _Rating = null;
        }

        public CommunityRating(double? rating)
        {
            _Rating = (null != rating) ? Math.Min(Math.Max(0, Math.Round(rating.Value, 1)), 5) : null;
        }

        // IXmlSerializable implementation for custom serialization as a double value
        public XmlSchema? GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            string content = reader.ReadElementContentAsString();
            if (double.TryParse(content, out double parsedValue))
            {
                Value = parsedValue;
            }
            else
            {
                Value = null;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Value.HasValue)
            {
                writer.WriteValue(Value.Value);
            }
        }
    }
}
