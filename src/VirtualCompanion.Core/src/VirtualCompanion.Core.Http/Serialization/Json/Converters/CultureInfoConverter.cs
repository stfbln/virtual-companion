using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VirtualCompanion.Core.Http.Serialization.Json.Converters
{
    public class CultureInfoJsonConverter : JsonConverter
    {
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CultureInfo);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value is CultureInfo cultureInfo)
            {
                writer.WriteValue(cultureInfo.Name);
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.Value is string stringValue)
            {
                return new CultureInfo(stringValue);
            }
            else
            {
                return CultureInfo.InvariantCulture;
            }
        }

        public override bool CanRead => true;
    }
}
