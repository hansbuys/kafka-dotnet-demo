namespace Kafka.Receiver.Service.Tests
{
    internal static class AssertionsProvider
    {
        internal static ConsoleAssertions Should(this FakeConsole subject)
        {
            return new ConsoleAssertions(subject);
        }
    }
}
