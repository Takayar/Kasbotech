using Kasbotech_RandCandle.MessagingBus.SendMessage;
using System;

namespace Kasbotech_RandCandle.MessagingBus.Messages
{
    public class SendOrderToPaymentMessage:BaseMessage
    {
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
    }
}
