using Kafka.Infrastructure;
using Kafka.Publisher.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kafka.Publisher.Service.Tests
{
    public class FakeProducer : IProduceMessages<string>, IDisposable
    {
        internal List<Message> ProducedMessages = new List<Message>();

        public Task Produce(string topic, string message)
        {
            ProducedMessages.Add(new Message(topic, message));

            return 0.Async();
        }

        public void Dispose()
        {
            ProducedMessages = null;
        }
    }
}
