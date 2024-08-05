using Kasbotech_Identity.MessagingBus.SendMessage;
using System;

namespace Kasbotech_Identity.MessagingBus.Messages
{
    public class SendUserIdMessage:BaseMessage
    {
        public Guid UserId { get; set; }
    }
}
