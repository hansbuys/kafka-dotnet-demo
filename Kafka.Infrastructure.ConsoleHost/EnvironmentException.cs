using System;

namespace Kafka.Infrastructure.ConsoleHost
{
    [Serializable]
    internal class EnvironmentException : Exception
    {
        public EnvironmentException() : base() { }
        public EnvironmentException(string message) : base(message) { }
        public EnvironmentException(string message, Exception innerException) : base(message, innerException) { }
    }
}