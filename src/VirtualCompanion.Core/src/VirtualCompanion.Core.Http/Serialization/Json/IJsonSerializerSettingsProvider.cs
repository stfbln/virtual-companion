using Newtonsoft.Json;

namespace VirtualCompanion.Core.Http.Serialization.Json
{
    public interface IJsonSerializerSettingsProvider
    {
        JsonSerializerSettings GetJsonSerializerSettings();
    }
}
