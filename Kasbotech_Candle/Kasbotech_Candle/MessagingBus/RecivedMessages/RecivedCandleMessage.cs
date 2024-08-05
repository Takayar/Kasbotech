using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Kasbotech_Candle.Model.Services;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using System;
using Newtonsoft.Json;
using System.Text;

namespace Kasbotech_Candle.MessagingBus.RecivedMessages
{
    public class RecivedCandleMessage : BackgroundService

    {
        private IModel _channel;
        private IConnection _connection;
        private string _hostname;
        private string _username;
        private string _password;
        private string _queueName;
        private string _exchangeName;
        private readonly ICandleService _candleService;

        public RecivedCandleMessage(IOptions<RabbitMqConfiguration> rabbitMqOptions,
            ICandleService candleAvgService)
        {
            _candleService = candleAvgService;
            _hostname = rabbitMqOptions.Value.HostName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _queueName = rabbitMqOptions.Value.QueueName_Rnd;
            _exchangeName = rabbitMqOptions.Value.ExchangeName_Candle;
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout, true, false);
            _channel.QueueDeclare(_queueName, true, false, false);
            _channel.QueueBind(_queueName, _exchangeName, "");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var AddCandle = JsonConvert.DeserializeObject<CandleDto>(content);

                var resultHandleMessage = HandleMessage(AddCandle);
                if (resultHandleMessage)
                    _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(_queueName, false, consumer);
            return Task.CompletedTask;
        }
        private bool HandleMessage(CandleDto candle)
        {
            return _candleService.AddNewCandle(candle);
        }
    }

    public class CandleDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string CandleName { get; set; }
        public decimal CandleOpen { get; set; }
        public decimal CandleClose { get; set; }
        public decimal CandleHigh { get; set; }
        public decimal CandleLow { get; set; }
        public decimal CandleAvg { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
