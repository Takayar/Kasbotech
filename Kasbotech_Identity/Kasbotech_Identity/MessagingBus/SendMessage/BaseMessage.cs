using System;

namespace Kasbotech_Identity.MessagingBus.SendMessage
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
