using System.Threading.Tasks;

namespace Kafka.Service.Interfaces
{
    public interface IWriteOutput
    {
        Task WriteOutput(string output);
    }
}
