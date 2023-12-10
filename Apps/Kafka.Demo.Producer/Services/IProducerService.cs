namespace Kafka.Demo.Producer.Services
{
    public interface IProducerService
    {
        Task SendAsync(string message, string? specifiedTopicName);
    }
}
