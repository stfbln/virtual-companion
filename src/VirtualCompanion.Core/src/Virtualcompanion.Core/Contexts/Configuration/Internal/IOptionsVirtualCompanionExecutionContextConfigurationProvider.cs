using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Virtualcompanion.Core.Contexts.Configuration.Internal
{
    public class OptionsVirtualCompanionExecutionContextConfigurationProvider : IVirtualCompanionExecutionContextConfigurationProvider
    {
        private readonly IOptions<VirtualCompanionExecutionContextConfiguration> _options;

        public OptionsVirtualCompanionExecutionContextConfigurationProvider(IOptions<VirtualCompanionExecutionContextConfiguration> options)
        {
            _options = options;
        }

        public VirtualCompanionExecutionContextConfiguration GetVirtualCompanionExecutionContextConfiguration()
        {
            return _options.Value;
        }
    }
}
