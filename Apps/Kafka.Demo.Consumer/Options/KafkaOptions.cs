namespace Kafka.Demo.Consumer.Options
{
    public class KafkaOptions
    {
        public string Topic { get; set; }
        public string Host { get; set; }
        public string GroupId { get; set; }
        public bool IsCommitEnabled { get; set; }
    }
}
