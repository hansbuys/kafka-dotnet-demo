using System.Threading.Tasks;

namespace Kafka.Service.Interfaces
{
    public interface IReadInput
    {
        Task<string> GetInput();
    }
}
