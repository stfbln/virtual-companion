using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Virtualcompanion.Core.Contexts.Features.Configuration
{
    public abstract class InputCultureFeatureConfigurationBase : CultureFeatureConfigurationBase
    {
        public InputCultureFeatureConfigurationBase(string featureTypeKey)
        {
            FeatureTypeKey = featureTypeKey;
        }

        public string InputTypeKey { get; set; } = "input";

        public virtual string FeatureTypeKey { get; set; }

        public override Func<CultureInfo, string> ContextKeyFactory => (culture) => $"input:{FeatureTypeKey}:{culture.TwoLetterISOLanguageName}";
    }
}
