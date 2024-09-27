using Chris82111.ComicInfo.Enums;
using Chris82111.Domain.Enums;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Chris82111.ComicInfo.Types
{
    public class AgeRatingType : IXmlSerializable
    {
        public AgeRating? Value
        {
            private set { _Rating = value; }
            get { return _Rating; }
        }

        public static implicit operator AgeRating?(AgeRatingType obj) => obj._Rating;
        public static implicit operator AgeRatingType(AgeRating? rating) => new AgeRatingType(rating);

        private AgeRating? _Rating;

        public AgeRatingType()
        {
            _Rating = null;
        }

        public AgeRatingType(AgeRating? rating)
        {
            _Rating = rating;
        }

        // IXmlSerializable implementation for custom serialization as a double value
        public XmlSchema? GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            string content = reader.ReadElementContentAsString();
            if (EnumHelper<AgeRating>.DisplayEnum.ContainsKey(content))
            {
                Value = EnumHelper<AgeRating>.DisplayEnum[content];
            }
            else if (EnumHelper<AgeRating>.StringEnum.ContainsKey(content))
            {
                Value = EnumHelper<AgeRating>.StringEnum[content];
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
                writer.WriteValue(EnumHelper<AgeRating>.EnumDisplay[Value.Value]);
            }
        }
    }
}
