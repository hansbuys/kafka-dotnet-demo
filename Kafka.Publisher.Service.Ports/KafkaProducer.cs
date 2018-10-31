using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Kafka.Publisher.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kafka.Publisher.Service.Ports
{
    public class KafkaProducer : IProduceMessages<string>
    {
        private readonly Producer<Null, string> producer;
        private readonly ILogger<KafkaProducer> log;

        public KafkaProducer(KafkaProducerConfiguration config, ILogger<KafkaProducer> log)
        {
            log.LogDebug($"Creation of Kafka producer, connecting to '{config.Servers}'.");
            producer = new Producer<Null, string>(config.Config, null, new StringSerializer(Encoding.UTF8));

            log.LogDebug("Attaching eventhandlers.");
            producer.OnError += (_, msg) =>
            {
                log.LogError($"{msg.Code}: {msg.Reason}.");
            };

            this.log = log;
        }

        public Task Produce(string topic, string message)
        {
            log.LogInformation($"Sending a message for topic '{topic}'.");
            return producer.ProduceAsync(topic, null, message);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    producer.Flush(100);
                    producer.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
