namespace Kafka.Receiver.Service.Interfaces
{
    public class Message<T>
    {
        public Message(string topic, T value)
        {
            Topic = topic;
            Value = value;
        }

        public T Value { get; }
        public string Topic { get; }
    }
}