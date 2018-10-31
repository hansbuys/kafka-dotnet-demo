using Kafka.Infrastructure.ConsoleHost;
using Kafka.Receiver.Service;

namespace Kafka.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildServiceHost(args).Run();
        }

        private static IConsoleHost BuildServiceHost(string[] args) =>
            ConsoleHost<KafkaReceiver>.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        
    }
}
