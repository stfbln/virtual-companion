using System;
using System.Threading.Tasks;

namespace Virtualcompanion.Core.Contexts.Extensibility
{
    public interface IVirtualCompanionExecutionContextProcessorHandler
    {
        Task HandleVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context, Func<Task> next);
    }
}
