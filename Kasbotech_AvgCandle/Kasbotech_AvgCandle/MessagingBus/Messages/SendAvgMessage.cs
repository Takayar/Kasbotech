using Kasbotech_AvgCandle.MessagingBus.SendMessage;
using System;

namespace Kasbotech_AvgCandle.MessagingBus.Messages
{
    public class SendAvgMessage:BaseMessage
    {
        public Guid CandleId { get; set; }
        public int Amount { get; set; }
    }
}
