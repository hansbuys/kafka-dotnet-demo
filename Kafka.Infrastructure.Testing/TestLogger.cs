using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace Kafka.Infrastructure.Testing
{
    public class TestLogger : ILogger
    {
        private readonly string name;
        private readonly ITestOutputHelper output;

        public TestLogger(string name, ITestOutputHelper output)
        {
            this.name = name;
            this.output = output;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            output.WriteLine($"{logLevel} - {formatter(state, exception)}");
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
