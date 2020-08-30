using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Virtualcompanion.Core.Contexts.Extensibility;
using VirtualCompanion.Core.Http.Contexts.Extensibility;
using VirtualCompanion.Core.Http.Serialization.Json;
using VirtualCompanion.Core.Http.Serialization.Json.Converters;
using VirtualCompanion.Core.Http.Serialization.Json.Converters.Internal;
using VirtualCompanion.Core.Http.Serialization.Json.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpVirtualCompanionExecutionContextServiceCollectionServant
    {
        private static VirtualCompanionBuilder AddHttp(this VirtualCompanionBuilder builder)
        {
            builder.Services.TryAddTransient<IVirtualCompanionExecutionContextSerializer, JsonVirtualCompanionExecutionContextSerializer>();
            builder.Services.TryAddTransient<IJsonSerializerSettingsProvider, JsonSerializerSettingsProvider>();
            builder.Services.TryAddTransient<IJsonConvertersProvider, JsonConvertersProvider>();

            return builder;
        }

        public static VirtualCompanionBuilder AddHttpContextProcess<TContextProcess>(this VirtualCompanionBuilder builder, Action<IHttpClientBuilder> setup = null)
            where TContextProcess : HttpVirtualCompanionExecutionContextProcessorHandlerBase
        {
            builder.AddHttp();

            var httpClientBuilder = builder.Services.AddHttpClient<IVirtualCompanionExecutionContextProcessorHandler, TContextProcess>();
            setup?.Invoke(httpClientBuilder);

            return builder;
        }

        public static VirtualCompanionBuilder AddHttpContextProcess<TContextProcess>(this VirtualCompanionBuilder builder, string name, Action<IHttpClientBuilder> setup = null)
            where TContextProcess : HttpVirtualCompanionExecutionContextProcessorHandlerBase
        {
            builder.AddHttp();

            var httpClientBuilder = builder.Services.AddHttpClient<IVirtualCompanionExecutionContextProcessorHandler, TContextProcess>(name);
            setup?.Invoke(httpClientBuilder);

            return builder;
        }

        public static VirtualCompanionBuilder SetContextSerializer<TContextSerializer>(this VirtualCompanionBuilder builder)
            where TContextSerializer : class, IVirtualCompanionExecutionContextSerializer
        {
            builder.Services.AddTransient<IVirtualCompanionExecutionContextSerializer, TContextSerializer>();

            return builder;
        }

        public static VirtualCompanionBuilder SetJsonSerializerSettingsProvider<TSerializerSettingsProvider>(this VirtualCompanionBuilder builder)
            where TSerializerSettingsProvider : class, IJsonSerializerSettingsProvider
        {
            builder.Services.AddTransient<IJsonSerializerSettingsProvider, TSerializerSettingsProvider>();

            return builder;
        }

        public static VirtualCompanionBuilder SetJsonConvertersProvider<TJsonConvertersProvider>(this VirtualCompanionBuilder builder)
            where TJsonConvertersProvider : class, IJsonConvertersProvider
        {
            builder.Services.AddTransient<IJsonConvertersProvider, TJsonConvertersProvider>();

            return builder;
        }
    }
}
