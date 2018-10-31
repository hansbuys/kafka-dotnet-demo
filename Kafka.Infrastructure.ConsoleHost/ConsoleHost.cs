using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kafka.Infrastructure.ConsoleHost
{
    public class ConsoleHost<TService> : IConsoleHost
        where TService : IService
    {
        private IServiceProvider serviceProvider;

        public ConsoleHost(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run(string[] args)
        {
            var service = serviceProvider.GetRequiredService<TService>();

            try
            {
                service.Start().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal exception: " + ex.Message);
                Console.WriteLine("Press return to exit...");
                Console.ReadLine();
            }
        }

        public static IConsoleHostBuilder CreateDefaultBuilder(string[] args = null)
        {
            return new ConsoleHostBuilder<TService>();
        }
    }
}
