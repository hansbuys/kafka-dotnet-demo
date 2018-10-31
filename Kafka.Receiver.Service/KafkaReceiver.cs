using System.Threading.Tasks;
using Kafka.Infrastructure;
using Kafka.Infrastructure.ConsoleHost;
using Kafka.Receiver.Service.Interfaces;
using Kafka.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kafka.Receiver.Service
{
    public class KafkaReceiver : IService
    {
        private readonly ILogger<KafkaReceiver> log;
        private readonly IReceiveMessages<string> receiver;
        private readonly IWriteOutput output;
        private readonly IReadInput input;

        public KafkaReceiver(ILogger<KafkaReceiver> log, IReceiveMessages<string> receiver, IWriteOutput output, IReadInput input)
        {
            this.log = log;
            this.receiver = receiver;
            this.output = output;
            this.input = input;
        }

        public async Task Start()
        {
            receiver.Subscribe(KafkaTopics.HelloTopic, HandleMessage);

            await output.WriteOutput("Press return to exit...");
            await input.GetInput();
        }

        private Task HandleMessage(Message<string> msg)
        {
            return output.WriteOutput($"Topic: {msg.Topic} - Message : {msg.Value}");
        }
    }
}