using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Kafka.Receiver.Service.Tests
{
    public class KafkaReceiverTests : KafkaReceiverTestBase
    {
        public KafkaReceiverTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("hello-topic", "hello world")]
        public async Task WhenMessageArrivesOutputIsLogged(string topic, string message)
        {
            var receiver = await GetSut();
            Console.QueueInput("");

            await receiver.Start();
            Consumer.WhenMessageArrives(topic, message);

            Console.Should().HaveWritten($"Topic: {topic} - Message : {message}");
        }
    }
}
