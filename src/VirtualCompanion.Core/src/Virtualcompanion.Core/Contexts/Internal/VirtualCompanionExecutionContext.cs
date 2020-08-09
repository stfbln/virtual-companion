using System;
using System.Collections.Generic;
using System.Text;
using Virtualcompanion.Core.Contexts.Configuration;

namespace Virtualcompanion.Core.Contexts.Internal
{
    public class VirtualCompanionExecutionContext : Dictionary<string, object>, IVirtualCompanionExecutionContext
    {
        public VirtualCompanionExecutionContextConfiguration Configuration { get; set; } = new VirtualCompanionExecutionContextConfiguration();
    }
}
