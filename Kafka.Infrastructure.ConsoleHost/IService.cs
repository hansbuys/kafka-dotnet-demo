using System.Threading.Tasks;

namespace Kafka.Infrastructure.ConsoleHost
{
    public interface IService
    {
        Task Start();
    }
}