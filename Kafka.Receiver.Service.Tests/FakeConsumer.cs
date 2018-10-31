using System;
using System.Threading.Tasks;
using Kafka.Receiver.Service.Interfaces;

namespace Kafka.Receiver.Service.Tests
{
    public class FakeConsumer : IReceiveMessages<string>
    {
        private event EventHandler<Message<string>> OnMessage;

        public void Subscribe(string helloTopic, Func<Message<string>, Task> handleMessage)
        {
            OnMessage += (_, msg) => {
                handleMessage(msg);
            };
        }

        internal void WhenMessageArrives(string topic, string message)
        {
            OnMessage(this, new Message<string>(topic, message));
        }
    }
}