using System;
using System.Threading.Tasks;

namespace Kafka.Receiver.Service.Interfaces
{
    public interface IReceiveMessages<T>
    {
        void Subscribe(string helloTopic, Func<Message<T>, Task> handleMessage);
    }
}
