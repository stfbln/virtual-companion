using System;
using System.Collections.Generic;
using System.Text;
using Virtualcompanion.Core.Contexts.Configuration;
using Virtualcompanion.Core.Contexts.Features;

namespace Virtualcompanion.Core.Contexts.Internal
{
    public class VirtualCompanionExecutionContext : Dictionary<string, IVirtualCompanionExecutionContextFeature>, IVirtualCompanionExecutionContext
    {
        public VirtualCompanionExecutionContextConfiguration Configuration { get; set; } = new VirtualCompanionExecutionContextConfiguration();
    }
}
