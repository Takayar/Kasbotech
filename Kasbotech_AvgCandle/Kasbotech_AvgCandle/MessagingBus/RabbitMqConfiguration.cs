namespace Kasbotech_AvgCandle.MessagingBus
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string QueueName_Rnd { get; set; }
        public string QueueName_Candle { get; set; }
        public string QueueName_UserIdentity { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ExchangeName_Avg { get; set; }
    }
}
