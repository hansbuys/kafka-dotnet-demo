using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Kafka.Publisher.Service.Tests
{
    public class KafkaPublisherTests : KafkaPublisherTestBase
    {
        public KafkaPublisherTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("Hello World")]
        public async Task MessagesCanBeProduced(string message)
        {
            var publisher = await GetSut();
            Console.QueueInput(message, "exit");

            await publisher.Start();

            Producer.Should()
                .HaveProducedASingleMessage().Which.Should()
                    .HaveTopic("hello-topic").And
                    .HaveValue(message);
        }

        [Fact]
        public async Task NoMessageIsProducedForExit()
        {
            var publisher = await GetSut();
            Console.QueueInput("exit");

            await publisher.Start();

            Producer.Should().HaveProducedNoMessages();
        }
    }
}
