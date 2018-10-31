using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Kafka.Infrastructure.Testing
{
    internal class TestLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper output;

        public TestLoggerProvider(ITestOutputHelper output)
        {
            this.output = output;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TestLogger(categoryName, output);
        }

        public void Dispose()
        {
        }
    }
}
