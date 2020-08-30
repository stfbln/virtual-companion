using System.Globalization;

namespace Virtualcompanion.Core.Contexts.Features
{
    public class TextInputFeature : VirtualCompanionExecutionContextFeatureBase
    {
        public CultureInfo Culture { get; set; }

        public string Text { get; set; }

        public override VirtualCompanionExecutionContextFeatureType Type => VirtualCompanionExecutionContextFeatureType.Input | VirtualCompanionExecutionContextFeatureType.Text;
    }
}
