using System.Collections.Generic;
using Virtualcompanion.Core.Contexts.Configuration;
using Virtualcompanion.Core.Contexts.Features;

namespace Virtualcompanion.Core.Contexts
{
    public interface IVirtualCompanionExecutionContext : IDictionary<string, IVirtualCompanionExecutionContextFeature>
    {
        VirtualCompanionExecutionContextConfiguration Configuration { get; set; }
    }
}
