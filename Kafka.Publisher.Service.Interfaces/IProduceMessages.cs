using System;
using System.Threading.Tasks;

namespace Kafka.Publisher.Service.Interfaces
{
    public interface IProduceMessages<T> : IDisposable
    {
        Task Produce(string topic, T message);
    }
}
