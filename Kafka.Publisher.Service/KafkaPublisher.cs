using System.Threading.Tasks;
using Kafka.Infrastructure;
using Kafka.Infrastructure.ConsoleHost;
using Kafka.Publisher.Service.Interfaces;
using Kafka.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kafka.Publisher.Service
{
    public class KafkaPublisher : IService
    {
        private readonly ILogger<KafkaPublisher> log;
        private readonly IReadInput input;
        private readonly IWriteOutput output;
        private readonly IProduceMessages<string> producer;

        public KafkaPublisher(ILogger<KafkaPublisher> log, IReadInput input, IWriteOutput output, IProduceMessages<string> producer)
        {
            this.log = log;
            this.input = input;
            this.output = output;
            this.producer = producer;
        }

        public async Task Start()
        {
            string value = null;
            const string exitValue = "exit";
            bool isExitValue(string val) => val == exitValue;

            do
            {
                await output.WriteOutput($"Enter some text or '{exitValue}' to quit: ");
                value = await input.GetInput();

                if (!isExitValue(value))
                    await producer.Produce(KafkaTopics.HelloTopic, value);
            } while (!isExitValue(value));
        }
    }
}