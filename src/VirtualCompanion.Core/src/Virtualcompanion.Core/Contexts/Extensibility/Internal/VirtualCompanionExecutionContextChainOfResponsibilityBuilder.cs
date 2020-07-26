using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtualcompanion.Core.Contexts.Extensibility.Internal
{
    public class VirtualCompanionExecutionContextChainOfResponsibilityBuilder
    {
        private readonly ICollection<IVirtualCompanionExecutionContextProcessorHandler> _handlers = new List<IVirtualCompanionExecutionContextProcessorHandler>();

        public void AddHandler(IVirtualCompanionExecutionContextProcessorHandler handler)
        {
            _handlers.Add(handler);
        }

        public Func<IVirtualCompanionExecutionContext, Task> Build()
        {
            // handler 1
            // handler 2

            return (context) => {

                Func<Task> next = () => Task.CompletedTask;

                foreach (var handler in _handlers.Reverse())
                {
                    var localNext = next; // variable is stored in local scope of foreach loop
                    next = () => handler.HandleVirtualCompanionExecutionContextAsync(context, localNext);
                }

                return next.Invoke();
            };
        }
    }
}
