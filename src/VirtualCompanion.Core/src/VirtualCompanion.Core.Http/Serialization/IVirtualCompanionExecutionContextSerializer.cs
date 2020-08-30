using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts;

namespace VirtualCompanion.Core.Http.Serialization.Json
{
    public interface IVirtualCompanionExecutionContextSerializer
    {
        Task<string> SerializeAsync(IVirtualCompanionExecutionContext context);

        Task<IVirtualCompanionExecutionContext> DeserializeAsync(string serializedContext);
    }
}
