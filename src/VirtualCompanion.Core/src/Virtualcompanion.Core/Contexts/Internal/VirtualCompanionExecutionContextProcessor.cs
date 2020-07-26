using System.Collections.Generic;
using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts.Extensibility;
using Virtualcompanion.Core.Contexts.Extensibility.Internal;

namespace Virtualcompanion.Core.Contexts.Internal
{
    public class VirtualCompanionExecutionContextProcessor : IVirtualCompanionExecutionContextProcessor
    {
        private readonly IEnumerable<IVirtualCompanionExecutionContextProcessorHandler> _handlers;

        public VirtualCompanionExecutionContextProcessor(IEnumerable<IVirtualCompanionExecutionContextProcessorHandler> handlers)
        {
            _handlers = handlers;
        }

        public async Task ProcessVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context)
        {
            var chainOfResponsibilityBuilder = new VirtualCompanionExecutionContextChainOfResponsibilityBuilder();

            foreach(var handler in _handlers)
            {
                chainOfResponsibilityBuilder.AddHandler(handler);
            }

            var builtChainOfResponsibility = chainOfResponsibilityBuilder.Build();
            await builtChainOfResponsibility.Invoke(context);
        }
    }
}
