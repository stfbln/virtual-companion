using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Virtualcompanion.Core.Contexts.Features;
using Virtualcompanion.Core.Contexts.Features.Audios;

namespace VirtualCompanion.Core.Http.Serialization.Json.Converters
{
    public class VirtualCompanionExecutionContextFeatureJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IVirtualCompanionExecutionContextFeature);
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            IVirtualCompanionExecutionContextFeature feature = null;

            if (reader.TokenType != JsonToken.Null)
            {
                var jObject = JObject.Load(reader);
                var type = (VirtualCompanionExecutionContextFeatureType)jObject["Type"].Value<Int64>();
                feature = CreateVirtualCompanionExecutionContextFeature(type);
                if (feature != null)
                {
                    using (JsonReader jObjectReader = jObject.CreateReader())
                    {
                        serializer.Populate(jObjectReader, feature);
                    }
                }
            }

            return feature;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private IVirtualCompanionExecutionContextFeature CreateVirtualCompanionExecutionContextFeature(VirtualCompanionExecutionContextFeatureType type)
        {
            if (type == (VirtualCompanionExecutionContextFeatureType.Input | VirtualCompanionExecutionContextFeatureType.Text))
            {
                return new TextInputFeature();
            }
            else if (type == (VirtualCompanionExecutionContextFeatureType.Input | VirtualCompanionExecutionContextFeatureType.Audio))
            {
                return new AudioInputFeature();
            }
            else
            {
                return null;
            }
        }
    }
}
