using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCompanion.Core.Http.Serialization.Json.Converters
{
    public interface IJsonConvertersProvider
    {
        IEnumerable<JsonConverter> GetJsonConverters();
    }
}
