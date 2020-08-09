using System;
using System.Globalization;

namespace Virtualcompanion.Core.Contexts.Features.Configuration
{
    public abstract class CultureFeatureConfigurationBase : FeatureConfiguration
    {
        public abstract Func<CultureInfo, string> ContextKeyFactory { get; }
    }
}
