using System;
using System.Threading.Tasks;

namespace Virtualcompanion.Core.Contexts.Extensibility
{
    public abstract class VirtualCompanionExecutionContextProcessorHandlerBase : IVirtualCompanionExecutionContextProcessorHandler
    {
        public virtual async Task HandleVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context, Func<Task> next)
        {
            await HandleDownstreamVirtualCompanionExecutionContextAsync(context);

            await HandleNextVirtualCompanionExecutionContextHandlerAsync(context, next);

            await HandleUpstreamVirtualCompanionExecutionContextAsync(context);
        }

        protected virtual Task HandleDownstreamVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context)
        {
            HandleDownstreamVirtualCompanionExecutionContext(context);
            return Task.CompletedTask;
        }

        protected virtual void HandleDownstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {

        }

        protected virtual Task HandleUpstreamVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context)
        {
            HandleUpstreamVirtualCompanionExecutionContext(context);
            return Task.CompletedTask;
        }

        protected virtual void HandleUpstreamVirtualCompanionExecutionContext(IVirtualCompanionExecutionContext context)
        {

        }

        protected virtual Task HandleNextVirtualCompanionExecutionContextHandlerAsync(IVirtualCompanionExecutionContext context, Func<Task> next)
        {
            return next.Invoke();
        }
    }
}
