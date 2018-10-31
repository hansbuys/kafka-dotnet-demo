using System;
using FluentAssertions;
using Kafka.Infrastructure.Testing;

namespace Kafka.Receiver.Service.Tests
{
    internal class ConsoleAssertions : Assertions<FakeConsole, ConsoleAssertions>
    {
        public ConsoleAssertions(FakeConsole subject) : base(subject)
        {
        }

        internal AndConstraint<ConsoleAssertions> HaveWritten(string message)
        {
            Subject.ConsoleOutput.Should().Contain(message);

            CheckedThat("a message was written to the console output");

            return And();
        }
    }
}