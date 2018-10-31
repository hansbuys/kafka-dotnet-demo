using FluentAssertions;
using Kafka.Infrastructure.Testing;

namespace Kafka.Publisher.Service.Tests
{
    internal class MessageAssertions : Assertions<Message, MessageAssertions>
    {
        public MessageAssertions(Message subject) : base(subject)
        {
        }

        internal AndConstraint<MessageAssertions> HaveTopic(string topic)
        {
            Subject.Topic.Should().Be(topic);

            return And();
        }

        internal AndConstraint<MessageAssertions> HaveValue(string value)
        {
            Subject.Value.Should().Be(value);

            return And();
        }
    }
}