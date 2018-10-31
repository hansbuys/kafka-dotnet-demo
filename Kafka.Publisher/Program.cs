using Kafka.Infrastructure.ConsoleHost;
using Kafka.Publisher.Service;

namespace Kafka.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildServiceHost(args).Run();
        }

        private static IConsoleHost BuildServiceHost(string[] args) =>
            ConsoleHost<KafkaPublisher>.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
    }
}
