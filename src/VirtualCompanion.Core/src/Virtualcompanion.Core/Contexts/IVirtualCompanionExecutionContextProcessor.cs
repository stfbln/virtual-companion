using System.Threading.Tasks;

namespace Virtualcompanion.Core.Contexts
{
    public interface IVirtualCompanionExecutionContextProcessor
    {
        Task ProcessVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context);
    }
}
