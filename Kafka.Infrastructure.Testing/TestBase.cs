using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Kafka.Infrastructure.Testing
{
    public abstract class TestBase
    {
        protected ILoggerProvider LoggerProvider { get; }

        protected TestBase(ITestOutputHelper output)
        {
            LoggerProvider = new TestLoggerProvider(output);

            Assertions.WriteTestOutput = output.WriteLine;
        }
    }
}
