using System.Collections.Generic;

namespace Kafka.Publisher.Service.Ports
{
    public class KafkaProducerConfiguration
    {
        public string Servers { get; set; }

        public IEnumerable<KeyValuePair<string, object>> Config =>
            new Dictionary<string, object>
            {
                { "bootstrap.servers", Servers }
            };
}
}
