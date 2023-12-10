using Confluent.Kafka;

namespace Kafka.Demo.Producer.Services
{
    public class ProducerService : IProducerService
    {
        private const string DefaultTopicName = "kafka.example";
        private const string Host = "localhost:9092";
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<ProducerService> _logger;

        public ProducerService(ILogger<ProducerService> logger)
        {
            _logger = logger;

            var config = new ProducerConfig()
            {
                BootstrapServers = Host
            };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendAsync(string message, string? specifiedTopicName)
        {
            var kafkaMessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            };

            var topicName = string.IsNullOrWhiteSpace(specifiedTopicName)
                ? DefaultTopicName
                : specifiedTopicName;

            var result = await _producer.ProduceAsync(topicName, kafkaMessage);
            _logger.LogInformation($"Kafka delivery status: {result.Status} to topic: {topicName}, message offset: {result.TopicPartitionOffset.Offset}");
        }
    }
}
