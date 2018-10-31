using System;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Kafka.Receiver.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kafka.Receiver.Service.Ports
{
    public class KafkaMessageReceiver : IReceiveMessages<string>
    {
        private readonly Consumer<Null, string> consumer;
        private readonly ILogger<KafkaMessageReceiver> log;

        public KafkaMessageReceiver(ILogger<KafkaMessageReceiver> log, KafkaReceiverConfiguration config)
        {
            this.log = log;

            consumer = new Consumer<Null, string>(config.Config, null, new StringDeserializer(Encoding.UTF8));
        }

        public void Subscribe(string topic, Func<Message<string>, Task> handleMessage)
        {
            log.LogInformation($"Subscribing to {topic}.");
            consumer.Subscribe(topic);

            log.LogDebug("Attaching eventhandlers.");
            consumer.OnMessage += async (_, msg) =>
            {
                log.LogDebug($"New message arrived for topic {msg.Topic}.");
                await handleMessage(new Message<string>(msg.Topic, msg.Value));

                await consumer.CommitAsync(msg);
                log.LogDebug($"Committed message with offset {msg.Offset}.");
            };
            consumer.OnError += (_, msg) => 
            {
                log.LogError($"{msg.Code}: {msg.Reason}.");
            };

            Task.Factory
                .StartNew(StartPolling, TaskCreationOptions.RunContinuationsAsynchronously);
        }

        private void StartPolling()
        {
            log.LogDebug("Start polling in the background.");

            while (true)
                consumer.Poll(100);
        }
    }
}
