using FluentAssertions;
using Kafka.Infrastructure.Testing;

namespace Kafka.Publisher.Service.Tests
{
    internal class FakeProducerAssertions : Assertions<FakeProducer, FakeProducerAssertions>
    {
        public FakeProducerAssertions(FakeProducer producer) : base(producer)
        {
        }

        internal AndWhichConstraint<FakeProducerAssertions, Message> HaveProducedASingleMessage()
        {
            var message = Subject.ProducedMessages.Should().ContainSingle();

            CheckedThat("a single message has been produced");

            return AndWhich(message.Subject);
        }

        internal AndConstraint<FakeProducerAssertions> HaveProducedNoMessages()
        {
            Subject.ProducedMessages.Should().BeEmpty();

            CheckedThat("no messages have been produced");

            return And();
        }
    }
}