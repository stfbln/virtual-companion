using System.Collections.Generic;
using Virtualcompanion.Core.Contexts.Configuration;

namespace Virtualcompanion.Core.Contexts
{
    public interface IVirtualCompanionExecutionContext : IDictionary<string, object>
    {
        VirtualCompanionExecutionContextConfiguration Configuration { get; set; }
    }
}
