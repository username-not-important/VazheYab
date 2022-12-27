using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VazheYab.Logic
{
    // the generic class used
    public class GenericClass<T> where T : new()
    {
        public virtual T Value { get; set; }

        public GenericClass()
        {
            Value = new T();
        }
    }

    // the class for XML serialization
    public class Configuration
    {
        public GenericClass<TimeSpan> Time { get; set; }

        public Configuration()
        {
            Time = new GenericClass<TimeSpan>();
        }
    }

    public class TimeSpanClass : GenericClass<TimeSpan> , IXmlSerializable
    {
        [XmlIgnore]
        public override TimeSpan Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        [XmlElement(ElementName = "Value")]
        public string StringValue
        {
            get
            {
                return Value.ToString(@"hh\:mm\:ss");
            }
            set
            {
                Value = TimeSpan.ParseExact(value, @"hh\:mm\:ss", CultureInfo.InvariantCulture);
            }
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.ReadStartElement();

            Value = TimeSpan.ParseExact(reader.ReadElementContentAsString("Value", String.Empty), @"hh\:mm\:ss", CultureInfo.InvariantCulture);

            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("Value", Value.ToString(@"hh\:mm\:ss"));
        }

    }

}
