using Kafka.Receiver.Service.Interfaces;
using Kafka.Receiver.Service.Ports;
using Kafka.Service.Interfaces;
using Kafka.Service.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Receiver.Service
{
    public static class ConfigurationExtenions
    {
        public static void AddKafkaConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("Kafka").Get<KafkaReceiverConfiguration>());
        }

        public static void AddComponents(this IServiceCollection services)
        {
            services.AddTransient<KafkaReceiver>();
        }

        public static void AddPorts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddKafkaConfig(configuration);

            services.AddTransient<IReceiveMessages<string>, KafkaMessageReceiver>();
            services.AddTransient<IWriteOutput, ConsoleInputOutput>();
            services.AddTransient<IReadInput, ConsoleInputOutput>();
        }
    }
}
