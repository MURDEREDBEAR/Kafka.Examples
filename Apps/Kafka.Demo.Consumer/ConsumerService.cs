using Confluent.Kafka;
using Kafka.Demo.Consumer.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kafka.Demo.Consumer
{
    public class ConsumerService : BackgroundService
    {
        private const string DefaultTopicName = "kafka.example";
        private const string DefaultHostName = "localhost:9092";
        private const string DefaultGroupId = nameof(ConsumerService);

        private readonly ILogger<ConsumerService> _logger;
        private readonly KafkaOptions _kafkaOptions;

        private static ConsumerConfig _config = new();
        private static IConsumer<string, string>? _consumer;

        public ConsumerService(ILogger<ConsumerService> logger, IOptions<KafkaOptions> kafkaOptions)
        {
            _logger = logger;
            _kafkaOptions = kafkaOptions.Value;

            ConfigureConnection();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            await Task.Run(() => Consume(stoppingToken), stoppingToken);

        private void ConfigureConnection()
        {
            SetConfig();
            var topicName = GetTopicName();

            _logger.LogInformation("BootstrapServers: {name}", _config.BootstrapServers);
            _logger.LogInformation("GroupId: {groupId}", _config.GroupId);
            _logger.LogInformation("Topic: {name}", topicName);

            _consumer = new ConsumerBuilder<string, string>(_config).Build();
            _consumer.Subscribe(new List<string> { topicName });
        }

        private void Consume(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer?.Consume(cancellationToken);
                    _logger.LogInformation("Received result: {key} and {data}, offset: {offset}", result.Message.Key, result.Message.Value, result.Offset);

                    if (!_kafkaOptions.IsCommitEnabled)
                        continue;

                    _consumer?.Commit(result);
                    _logger.LogInformation("Saved offset");
                }
                catch (Exception e)
                {
                    _logger.LogInformation("Consumer error: {message}", e.Message);
                }
            }

            _consumer?.Close();
        }

        private void SetConfig()
        {
            _config = new()
            {
                BootstrapServers = GetHost(),
                GroupId = GetGroupId(),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoOffsetStore = false
            };
        }

        private string GetHost() => string.IsNullOrWhiteSpace(_kafkaOptions.Host)
            ? DefaultHostName
            : _kafkaOptions.Host;

        private string GetGroupId() => string.IsNullOrWhiteSpace(_kafkaOptions.GroupId)
            ? DefaultGroupId
            : _kafkaOptions.GroupId;

        private string GetTopicName() => string.IsNullOrWhiteSpace(_kafkaOptions.Topic)
            ? DefaultTopicName
            : _kafkaOptions.Topic;
    }
}
