using System.Collections.Generic;

namespace Kafka.Receiver.Service.Ports
{
    public class KafkaReceiverConfiguration
    {
        public string GroupId { get; set; }
        public string Servers { get; set; }
        public bool AutoCommit { get; set; }

        public IEnumerable<KeyValuePair<string, object>> Config =>
            new Dictionary<string, object>
            {
                { "group.id", GroupId },
                { "bootstrap.servers", Servers },
                { "enable.auto.commit", AutoCommit }
            };
    }
}
