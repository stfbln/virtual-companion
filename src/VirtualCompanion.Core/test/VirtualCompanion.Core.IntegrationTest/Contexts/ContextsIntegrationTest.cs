using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts;
using Virtualcompanion.Core.Contexts.Extensibility;
using Virtualcompanion.Core.Contexts.Internal;

namespace VirtualCompanion.Core.IntegrationTest.Contexts
{
    [TestClass]
    public class ContextsIntegrationTest
    {
        [TestMethod]
        public async Task Test()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IVirtualCompanionExecutionContextProcessorHandler, ConvertAudioPtToTextPtVirtualCompanionExecutionContextProcessorHandler>();
            serviceCollection.AddTransient<IVirtualCompanionExecutionContextProcessorHandler, ConvertAudioEnToTextEnVirtualCompanionExecutionContextProcessorHandler>();
            serviceCollection.AddTransient<IVirtualCompanionExecutionContextProcessorHandler, ConvertTextToEnVirtualCompanionExecutionContextProcessorHandler>();
            serviceCollection.AddTransient<IVirtualCompanionExecutionContextProcessorHandler, FinalProcessorVirtualCompanionExecutionContextProcessorHandler>();

            serviceCollection.AddTransient<VirtualCompanionExecutionContextProcessor>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var factory = new VirtualCompanionExecutionContextFactory();
            var context = await factory.CreateVirtualCompanionExecutionContextAsync();

            context["input:audio:pt"] = new byte[2048];

            var processor = serviceProvider.GetRequiredService<VirtualCompanionExecutionContextProcessor>();

            await processor.ProcessVirtualCompanionExecutionContextAsync(context);

            context.TryGetValue("output:text:en", out var v);
            Assert.IsInstanceOfType(v, typeof(string));
            Assert.AreEqual(v, "Hello You");
        }
    }

    public class ConvertAudioPtToTextPtVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleDownstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            if (context.TryGetValue("input:audio:pt", out var audio))
            {
                // convert audio to text
                context["input:text:pt"] = "Brazil";
            }
        }
    }

    public class ConvertAudioEnToTextEnVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleDownstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            if (context.TryGetValue("input:audio:en", out var audio))
            {
                // convert audio to text
                context["input:text:en"] = "Hello World";
            }
        }
    }

    public class ConvertTextToEnVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleDownstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            if (!context.ContainsKey("input:text:en"))
            {
                var textKeys = context.Keys.Where((k) => k.StartsWith("input:text"));
                var textValue = context[textKeys.First()];

                // translate text value
                context["input:text:en"] = "Hello World";
            }
        }
    }

    public class ConvertTextOutputFromEnToPtVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        protected override void HandleUpstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {
            // translate text value
            context["output:text:pt"] = "Portugal";
        }
    }

    public class FinalProcessorVirtualCompanionExecutionContextProcessorHandler : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        public override Task HandleVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context, Func<Task> next)
        {
            // output after processing
            context["output:text:en"] = "Hello You";
            return Task.CompletedTask;
        }
    }
}
