using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts;
using Virtualcompanion.Core.Contexts.Configuration;
using Virtualcompanion.Core.Contexts.Configuration.Internal;
using Virtualcompanion.Core.Contexts.Extensibility;
using Virtualcompanion.Core.Contexts.Features;
using Virtualcompanion.Core.Contexts.Internal;
using VirtualCompanion.Core.Http.Serialization.Json.Converters;

namespace VirtualCompanion.Core.IntegrationTest.Contexts
{
    [TestClass]
    public class ContextsIntegrationTest
    {
        [TestMethod]
        public async Task Test()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddVirtualCompanion()
                .AddContextProcess<ConvertAudioPtToTextPtVirtualCompanionExecutionContextProcessorHandler>()
                .AddContextProcess<ConvertAudioEnToTextEnVirtualCompanionExecutionContextProcessorHandler>()
                .AddContextProcess<ConvertTextToEnVirtualCompanionExecutionContextProcessorHandler>()
                .AddContextProcess<FinalProcessorVirtualCompanionExecutionContextProcessorHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var factory = serviceProvider.GetRequiredService<IVirtualCompanionExecutionContextFactory>();
            var context = await factory.CreateVirtualCompanionExecutionContextAsync();

            context.AddAudioInputFeature("pt", new byte[2048]);

            var processor = serviceProvider.GetRequiredService<IVirtualCompanionExecutionContextProcessor>();

            await processor.ProcessVirtualCompanionExecutionContextAsync(context);

            context.TryGetValue("output:text:en", out var v);
            //Assert.IsInstanceOfType(v, typeof(string));
            //Assert.AreEqual(v, "Hello You");


            var json = JsonConvert.SerializeObject(context, new JsonSerializerSettings { 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = {
                    new CultureInfoJsonConverter(),
                    new VirtualCompanionExecutionContextJsonConverter(serviceProvider.GetRequiredService<IVirtualCompanionExecutionContextFactory>())
                }
            });

            var newcontext = JsonConvert.DeserializeObject<IVirtualCompanionExecutionContext>(json, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = {
                    new CultureInfoJsonConverter(),
                    new VirtualCompanionExecutionContextJsonConverter(serviceProvider.GetRequiredService<IVirtualCompanionExecutionContextFactory>()),
                    new VirtualCompanionExecutionContextFeatureJsonConverter()
                }
            });
        }
    }

    public class ConvertAudioPtToTextPtVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleDownstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            if (context.TryGetAudioInputFeature("pt", out var audio))
            {
                // convert audio to text
                context.AddTextInputFeature("pt", "Brazil");
            }
        }
    }

    public class ConvertAudioEnToTextEnVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleDownstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            if (context.TryGetAudioInputFeature("en", out var audio))
            {
                // convert audio to text
                context.AddTextInputFeature("en", "Hello World");
            }
        }
    }

    public class ConvertTextToEnVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleDownstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            if (!context.TryGetTextInputFeature("en", out _))
            {
                var features = context.GetFeatures<TextInputFeature>();
                var textValue = features?.FirstOrDefault()?.Text;

                // translate text value
                context.AddTextInputFeature("en", "Hello World");
            }
        }
    }

    public class ConvertTextOutputFromEnToPtVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleUpstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            // translate text value
            context["output:text:pt"] = null;
        }
    }

    public class FinalProcessorVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        public override Task HandleVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context, Func<Task> next)
        {
            // output after processing
            context["output:text:en"] = null;
            return Task.CompletedTask;
        }
    }

    public class CustomVirtualCompanionExecutionContextConfigurationProvider : IVirtualCompanionExecutionContextConfigurationProvider
    {
        public VirtualCompanionExecutionContextConfiguration GetVirtualCompanionExecutionContextConfiguration()
        {
            throw new NotImplementedException();
        }
    }

    public class Custom2VirtualCompanionExecutionContextConfigurationProvider : IVirtualCompanionExecutionContextConfigurationProvider
    {
        public VirtualCompanionExecutionContextConfiguration GetVirtualCompanionExecutionContextConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
