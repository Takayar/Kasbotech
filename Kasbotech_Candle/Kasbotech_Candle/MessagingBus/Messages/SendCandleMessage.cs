using Kasbotech_Candle.MessagingBus.SendMessage;
using System;

namespace Kasbotech_Candle.MessagingBus.Messages
{
    public class SendCandleMessage:BaseMessage
    {
        public Guid CandleId { get; set; }
        public int Amount { get; set; }
    }
}
