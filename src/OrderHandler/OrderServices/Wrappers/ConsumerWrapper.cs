using Confluent.Kafka;

namespace OrderServices.Wrappers
{
    public class ConsumerWrapper
    {
        private string _topicName;
        private readonly Consumer<string, string> _consumer;
        public ConsumerWrapper(ConsumerConfig config, string topicName)
        {
            _topicName = topicName;
            _consumer = new Consumer<string, string>(config);
            _consumer.Subscribe(topicName);
        }
        public string ReadMessage()
        {
            var consumeResult = _consumer.Consume();
            return consumeResult.Value;
        }
    }
}
