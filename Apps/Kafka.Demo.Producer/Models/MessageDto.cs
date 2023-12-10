namespace Kafka.Demo.Producer.Models
{
    public class MessageDto
    {
        public string Value { get; set; } = "";
        public string? SpecifiedTopicName { get; set; }
    }
}
