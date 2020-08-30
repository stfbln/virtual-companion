using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualCompanion.Core.Http.Serialization.Json.Converters;

namespace VirtualCompanion.Core.Http.Serialization.Json.Internal
{
    class JsonSerializerSettingsProvider : IJsonSerializerSettingsProvider
    {
        private readonly IJsonConvertersProvider _jsonConvertersProvider;

        public JsonSerializerSettingsProvider(IJsonConvertersProvider jsonConvertersProvider)
        {
            _jsonConvertersProvider = jsonConvertersProvider;
        }

        public JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = _jsonConvertersProvider.GetJsonConverters().ToList()
            };
        }
    }
}
