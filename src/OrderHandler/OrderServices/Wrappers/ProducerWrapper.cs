using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace OrderServices.Wrappers
{
    public class ProducerWrapper
    {
        private readonly string _topicName;
        private readonly Producer<string, string> _producer;
        private readonly ProducerConfig _config;
        private static readonly Random rand = new Random();

        public ProducerWrapper(ProducerConfig config, string topicName)
        {
            _topicName = topicName;
            _config = config;
            _producer = new Producer<string, string>(_config);
            _producer.OnError += (_, e) =>
            {
                Console.WriteLine("Exception:" + e);
            };
        }
        public async Task WriteMessage(string message)
        {
            var dr = await _producer.ProduceAsync(_topicName, new Message<string, string>()
            {
                Key = rand.Next(5).ToString(),
                Value = message
            });
            Console.WriteLine($"KAFKA => Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
    }
}
