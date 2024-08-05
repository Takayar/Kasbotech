
using System.Collections.Generic;
using Kasbotech_AvgCandle.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kasbotech_AvgCandle.Infrastructure.Context
{
    public class CandleDataBaseContext : DbContext
    {
        public CandleDataBaseContext(DbContextOptions<CandleDataBaseContext> options) : base(options)
        {

        }

        public DbSet<Candle> Candles { get; set; }
    }
}
