using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Virtualcompanion.Core.Contexts;
using Virtualcompanion.Core.Contexts.Internal;

namespace VirtualCompanion.Core.Http.Serialization.Json.Converters
{
    public class VirtualCompanionExecutionContextJsonConverter : JsonConverter
    {
        private readonly IVirtualCompanionExecutionContextFactory _virtualCompanionExecutionContextFactory;

        public VirtualCompanionExecutionContextJsonConverter(IVirtualCompanionExecutionContextFactory virtualCompanionExecutionContextFactory)
        {
            _virtualCompanionExecutionContextFactory = virtualCompanionExecutionContextFactory;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IVirtualCompanionExecutionContext).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var context = _virtualCompanionExecutionContextFactory.CreateVirtualCompanionExecutionContextAsync().GetAwaiter().GetResult();

            if (reader.TokenType != JsonToken.Null)
            {
                var jObject = JObject.Load(reader);
                using (JsonReader jObjectReader = jObject.CreateReader())
                {
                    serializer.Populate(jObjectReader, context);
                }
            }

            return context;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
