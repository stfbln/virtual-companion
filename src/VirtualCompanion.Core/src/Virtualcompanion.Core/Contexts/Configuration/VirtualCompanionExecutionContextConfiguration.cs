using System;
using System.Collections.Generic;
using System.Text;
using Virtualcompanion.Core.Contexts.Features.Audios.Configuration;
using Virtualcompanion.Core.Contexts.Features.Texts.Configuration;

namespace Virtualcompanion.Core.Contexts.Configuration
{
    public class VirtualCompanionExecutionContextConfiguration
    {
        public TextInputFeatureConfiguration TextInputFeature { get; set; } = new TextInputFeatureConfiguration();

        public AudioInputFeatureConfiguration AudioInputFeature { get; set; } = new AudioInputFeatureConfiguration();
    }
}
