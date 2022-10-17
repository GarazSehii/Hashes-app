namespace HashHandler.Shared.Configuration
{
    public class RabbitMqOptions
    {
        public const string RabbitMqSection = "RabbitMq";

        public string HostName { get; set; } = string.Empty;
        public string Queue { get; set; } = string.Empty;
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public string Exchange { get; set; } = string.Empty;
        public bool Multiple { get; set; }
        public bool AutoAck { get; set; }
    }
}