using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Virtualcompanion.Core.Contexts;
using Virtualcompanion.Core.Contexts.Configuration;
using Virtualcompanion.Core.Contexts.Configuration.Internal;
using Virtualcompanion.Core.Contexts.Extensibility;
using Virtualcompanion.Core.Contexts.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class VirtualCompanionExecutionContextServiceCollectionServant
    {
        public static VirtualCompanionBuilder AddVirtualCompanion(this IServiceCollection services, Action<VirtualCompanionExecutionContextConfiguration> setup = null)
        {
            services.AddOptions();
            services.Configure<VirtualCompanionExecutionContextConfiguration>((o) => {
                setup?.Invoke(o);
            });

            services.TryAddTransient<IVirtualCompanionExecutionContextConfigurationProvider, OptionsVirtualCompanionExecutionContextConfigurationProvider>();

            services.TryAddTransient<IVirtualCompanionExecutionContextFactory, VirtualCompanionExecutionContextFactory>();
            services.TryAddTransient<IVirtualCompanionExecutionContextProcessor, VirtualCompanionExecutionContextProcessor>();

            return new VirtualCompanionBuilder { 
                Services = services
            };
        }

        public static VirtualCompanionBuilder ConfigureContext(this VirtualCompanionBuilder builder, Action<VirtualCompanionExecutionContextConfiguration> setup)
        {
            builder.Services.Configure<VirtualCompanionExecutionContextConfiguration>((o) => {
                setup.Invoke(o);
            });

            return builder;
        }

        public static VirtualCompanionBuilder SetContextConfigurationProvider<TContextConfigurationProvider>(this VirtualCompanionBuilder builder)
            where TContextConfigurationProvider : class, IVirtualCompanionExecutionContextConfigurationProvider
        {
            builder.Services.AddTransient<IVirtualCompanionExecutionContextConfigurationProvider, TContextConfigurationProvider>();

            return builder;
        }

        public static VirtualCompanionBuilder SetContextFactory<TContextFactory>(this VirtualCompanionBuilder builder)
            where TContextFactory : class, IVirtualCompanionExecutionContextFactory
        {
            builder.Services.AddTransient<IVirtualCompanionExecutionContextFactory, TContextFactory>();

            return builder;
        }

        public static VirtualCompanionBuilder SetContextProcessor<TContextProcessor>(this VirtualCompanionBuilder builder)
            where TContextProcessor : class, IVirtualCompanionExecutionContextProcessor
        {
            builder.Services.AddTransient<IVirtualCompanionExecutionContextProcessor, TContextProcessor>();

            return builder;
        }

        public static VirtualCompanionBuilder AddContextProcess<TContextProcess>(this VirtualCompanionBuilder builder)
            where TContextProcess : class, IVirtualCompanionExecutionContextProcessorHandler
        {
            builder.Services.AddTransient<IVirtualCompanionExecutionContextProcessorHandler, TContextProcess>();

            return builder;
        }
    }

    public class VirtualCompanionBuilder
    {
        public IServiceCollection Services { get; set; }
    }
}
