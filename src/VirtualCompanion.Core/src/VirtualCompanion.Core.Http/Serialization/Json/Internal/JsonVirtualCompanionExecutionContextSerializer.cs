using Newtonsoft.Json;
using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts;

namespace VirtualCompanion.Core.Http.Serialization.Json.Internal
{
    public class JsonVirtualCompanionExecutionContextSerializer : IVirtualCompanionExecutionContextSerializer
    {
        private readonly IJsonSerializerSettingsProvider _jsonSerializerSettingsProvider;

        public JsonVirtualCompanionExecutionContextSerializer(IJsonSerializerSettingsProvider jsonSerializerSettingsProvider)
        {
            _jsonSerializerSettingsProvider = jsonSerializerSettingsProvider;
        }

        public Task<IVirtualCompanionExecutionContext> DeserializeAsync(string serializedContext)
        {
            var settings = _jsonSerializerSettingsProvider.GetJsonSerializerSettings();
            var context = JsonConvert.DeserializeObject<IVirtualCompanionExecutionContext>(serializedContext, settings);
            return Task.FromResult(context);
        }

        public Task<string> SerializeAsync(IVirtualCompanionExecutionContext context)
        {
            var settings = _jsonSerializerSettingsProvider.GetJsonSerializerSettings();
            var json = JsonConvert.SerializeObject(context, settings);

            return Task.FromResult(json);
        }
    }
}
