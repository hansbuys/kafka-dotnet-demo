namespace Kafka.Publisher.Service.Tests
{
    internal class Message
    {
        public string Topic { get; }
        public string Value { get; }

        public Message(string topic, string value)
        {
            Topic = topic;
            Value = value;
        }
    }
}