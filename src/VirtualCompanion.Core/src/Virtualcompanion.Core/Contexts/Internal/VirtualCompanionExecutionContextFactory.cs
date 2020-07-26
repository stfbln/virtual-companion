using System.Threading.Tasks;

namespace Virtualcompanion.Core.Contexts.Internal
{
    public class VirtualCompanionExecutionContextFactory : IVirtualCompanionExecutionContextFactory
    {
        public virtual Task<IVirtualCompanionExecutionContext> CreateVirtualCompanionExecutionContextAsync()
        {
            var context = new VirtualCompanionExecutionContext();
            return Task.FromResult<IVirtualCompanionExecutionContext>(context);
        }
    }
}
