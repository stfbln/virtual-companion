using Newtonsoft.Json;
using System.Collections.Generic;

namespace VirtualCompanion.Core.Http.Serialization.Json.Converters.Internal
{
    public class JsonConvertersProvider : IJsonConvertersProvider
    {
        private readonly IEnumerable<JsonConverter> _jsonConverters;

        public JsonConvertersProvider(IEnumerable<JsonConverter> jsonConverters)
        {
            _jsonConverters = jsonConverters;
        }

        public IEnumerable<JsonConverter> GetJsonConverters()
        {
            return _jsonConverters;
        }
    }
}
