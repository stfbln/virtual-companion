using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts.Configuration;

namespace Virtualcompanion.Core.Contexts.Internal
{
    public class VirtualCompanionExecutionContextFactory : IVirtualCompanionExecutionContextFactory
    {
        private readonly IVirtualCompanionExecutionContextConfigurationProvider _configurationProvider;

        public VirtualCompanionExecutionContextFactory(IVirtualCompanionExecutionContextConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public virtual Task<IVirtualCompanionExecutionContext> CreateVirtualCompanionExecutionContextAsync()
        {
            var configuration = _configurationProvider.GetVirtualCompanionExecutionContextConfiguration();
            var context = new VirtualCompanionExecutionContext { 
                Configuration = configuration
            };

            return Task.FromResult<IVirtualCompanionExecutionContext>(context);
        }
    }
}
