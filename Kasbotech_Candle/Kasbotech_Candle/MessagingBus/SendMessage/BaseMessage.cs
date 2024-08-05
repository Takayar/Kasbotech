using System;

namespace Kasbotech_Candle.MessagingBus.SendMessage
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public DateTime Creationtime { get; set; } = DateTime.Now;
    }
}
