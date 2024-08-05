using Kasbotech_AvgCandle.Infrastructure.Context;
using System.ComponentModel;
using Kasbotech_AvgCandle.Model.Entities;
using System.Reflection.Metadata;
using System;
using Microsoft.AspNetCore.Authorization;


namespace Kasbotech_AvgCandle.Model.Services
{
    public interface ICandleAvgService
    {

        bool AddNewCandle(MessagingBus.RecivedMessages.CandleDto candle);
    }
    public class CandleAvgService : BackgroundService, ICandleAvgService
    {
        private readonly ILogger<ICandleAvgService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(2); 
        private readonly CandleDataBaseContext _context;
 
        public CandleAvgService(CandleDataBaseContext context,ILogger<ICandleAvgService> logger)
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
                _logger.LogInformation("Timed Background Service is working.");

               AvgCandle();
                await Task.Delay(_interval, stoppingToken);
            }
        }
        public void AvgCandle()
        {
            var lastCandle=_context.Candles.OrderByDescending(p=>p.Timestamp)
                .FirstOrDefault();
            if (lastCandle != null)
            {
                var result = _context.Candles.Where(p =>
                        p.Timestamp >= lastCandle.Timestamp.AddMinutes(-3) &&
                        p.Timestamp <= lastCandle.Timestamp)
                    .ToList();
                foreach (var val in result)
                {
                    decimal[] value = { val.CandleOpen,val.CandleClose,val.CandleHigh,val.CandleLow };


                    val.CandleAvg = value.Average();
                }
                    _context.SaveChanges();
            }
        }
    }
}
