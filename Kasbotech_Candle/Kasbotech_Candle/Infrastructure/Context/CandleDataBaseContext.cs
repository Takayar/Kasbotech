using Kasbotech_Candle.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kasbotech_Candle.Infrastructure.Context
{
    public class CandleDataBaseContext:DbContext
    {
        public CandleDataBaseContext(DbContextOptions<CandleDataBaseContext> options):base(options)
        {
            
        }

        public DbSet<Candle> Candles { get; set; }
    }
}
