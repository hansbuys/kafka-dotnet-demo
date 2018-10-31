using Kafka.Infrastructure.Testing;
using Kafka.Receiver.Service.Interfaces;
using Kafka.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Kafka.Receiver.Service.Tests
{
    public abstract class KafkaReceiverTestBase : IoCBasedTest<KafkaReceiver>
    {
        protected FakeConsumer Consumer { get; }
        protected FakeConsole Console { get; }

        public KafkaReceiverTestBase(ITestOutputHelper output) : base(output)
        {
            Consumer = new FakeConsumer();
            Console = new FakeConsole(output);
        }

        protected override void AddComponents(IServiceCollection services)
        {
            services.AddComponents();
        }

        protected override void AddPorts(IServiceCollection services)
        {
            services.AddSingleton<IReceiveMessages<string>>(Consumer);
            services.AddSingleton<IWriteOutput>(Console);
            services.AddSingleton<IReadInput>(Console);
        }
    }
}
