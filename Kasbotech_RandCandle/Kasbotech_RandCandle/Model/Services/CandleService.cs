using Kasbotech_RandCandle.Infrastructure.Context;
using System.ComponentModel;
using Kasbotech_RandCandle.Model.Entities;
using System.Reflection.Metadata;


namespace Kasbotech_RandCandle.Model.Services
{


    public class CandleRandService : BackgroundService
    {
        private readonly ILogger<CandleRandService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(2); // تنظیم زمان اجرا به 2 دقیقه
        private readonly CandleDataBaseContext _context;

        public CandleRandService(CandleDataBaseContext context, ILogger<CandleRandService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public bool AddNewCandle(MessagingBus.RecivedMessages.CandleDto candles)
        {
            Candle candle = new Candle()
            {
                CandleClose = candles.CandleClose,
                CandleHigh = candles.CandleHigh,
                CandleLow = candles.CandleLow,
                CandleName = candles.CandleName,
                CandleOpen = candles.CandleOpen,
                UserId = "1",

            };
            _context.Add(candle);
            _context.SaveChanges();
            return true;
        }
  
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Timed Background Service is working.-----------");

                AddNewCandle();
                _logger.LogInformation("باموفقیت ثبت شد");
                await Task.Delay(_interval, stoppingToken);
            }
        }
        public void AddNewCandle()
        {
            try
            {
                var lastDay = _context.Candles.OrderByDescending(p => p.Timestamp)
                    .FirstOrDefault();
                var highValue = (int)lastDay.CandleClose + ((int)lastDay.CandleClose * 0.05);
                Random random = new Random();

                Candle candle = new Candle()
                {
                    CandleOpen = lastDay.CandleClose,
                    CandleHigh = random.Next((int)lastDay.CandleClose, (int)highValue),
                    CandleLow = random.Next((int)lastDay.CandleClose, (int)highValue),
                    CandleClose = random.Next((int)lastDay.CandleClose, (int)highValue),
                    CandleName = "",
                    UserId = "1",

                };
                _context.Add(candle);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
               _logger.LogError("در ثبت اطلاعات خطایی به وجود آمد چیام:{e}");
                throw;
            }
         
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

    }
}
