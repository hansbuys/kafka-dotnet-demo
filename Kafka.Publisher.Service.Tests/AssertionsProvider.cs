namespace Kafka.Publisher.Service.Tests
{
    internal static class AssertionsProvider
    {
        internal static KafkaPublisherAssertions Should(this KafkaPublisher subject)
        {
            return new KafkaPublisherAssertions(subject);
        }

        internal static FakeProducerAssertions Should(this FakeProducer subject)
        {
            return new FakeProducerAssertions(subject);
        }

        internal static MessageAssertions Should(this Message subject)
        {
            return new MessageAssertions(subject);
        }
    }
}
