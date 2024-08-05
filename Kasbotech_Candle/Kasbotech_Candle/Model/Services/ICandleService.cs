using Kasbotech_Candle.Infrastructure.Context;
using System.ComponentModel;
using Kasbotech_Candle.Model.Entities;

namespace Kasbotech_Candle.Model.Services
{
    public interface ICandleService
    {
        void AddNewCandle(CandleDto  candles);
        bool AddNewCandle(MessagingBus.RecivedMessages.CandleDto candle);

    }

    public class CandleService : ICandleService
    {
        private readonly CandleDataBaseContext _context;

        public CandleService(CandleDataBaseContext context)
        {
            _context = context;
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

        public void AddNewCandle(CandleDto candles)
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
    }
}
