using System.Threading.Tasks;

namespace Virtualcompanion.Core.Contexts
{
    public interface IVirtualCompanionExecutionContextFactory
    {
        Task<IVirtualCompanionExecutionContext> CreateVirtualCompanionExecutionContextAsync();
    }
}
