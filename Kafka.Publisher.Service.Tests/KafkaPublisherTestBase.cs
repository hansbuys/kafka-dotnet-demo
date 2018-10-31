using Kafka.Infrastructure.Testing;
using Kafka.Publisher.Service.Interfaces;
using Kafka.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Kafka.Publisher.Service.Tests
{
    public abstract class KafkaPublisherTestBase : IoCBasedTest<KafkaPublisher>
    {
        protected FakeProducer Producer { get; }
        protected FakeConsole Console { get; }

        public KafkaPublisherTestBase(ITestOutputHelper output) : base(output)
        {
            Producer = new FakeProducer();
            Console = new FakeConsole();
        }

        protected override void AddComponents(IServiceCollection services)
        {
            services.AddComponents();
        }

        protected override void AddPorts(IServiceCollection services)
        {
            services.AddSingleton<IProduceMessages<string>>(Producer);
            services.AddSingleton<IWriteOutput>(Console);
            services.AddSingleton<IReadInput>(Console);
        }
    }
}
