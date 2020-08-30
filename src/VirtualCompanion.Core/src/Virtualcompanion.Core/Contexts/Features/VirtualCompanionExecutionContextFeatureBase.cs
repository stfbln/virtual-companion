namespace Virtualcompanion.Core.Contexts.Features
{
    public abstract class VirtualCompanionExecutionContextFeatureBase : IVirtualCompanionExecutionContextFeature
    {
        protected VirtualCompanionExecutionContextFeatureBase()
        {

        }

        public abstract VirtualCompanionExecutionContextFeatureType Type { get; }
    }
}
