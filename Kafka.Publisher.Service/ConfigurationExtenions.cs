using Kafka.Publisher.Service.Interfaces;
using Kafka.Publisher.Service.Ports;
using Kafka.Service.Interfaces;
using Kafka.Service.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Publisher.Service
{
    public static class ConfigurationExtenions
    {
        public static void AddComponents(this IServiceCollection services)
        {
            services.AddTransient<KafkaPublisher>();
        }

        public static void AddPorts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddKafkaConfig(configuration);
            services.AddTransient<KafkaPublisher>();

            services.AddTransient<IWriteOutput, ConsoleInputOutput>();
            services.AddTransient<IReadInput, ConsoleInputOutput>();

            services.AddTransient<IProduceMessages<string>, KafkaProducer>();
        }

        public static void AddKafkaConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("Kafka").Get<KafkaProducerConfiguration>());
        }
    }
}
