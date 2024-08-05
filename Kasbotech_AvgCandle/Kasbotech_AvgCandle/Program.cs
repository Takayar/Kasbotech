
using Kasbotech_AvgCandle.Infrastructure.Context;
using Kasbotech_AvgCandle.Model.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICandleAvgService, CandleAvgService>();
builder.Services.AddHostedService<CandleAvgService>();



IConfiguration Configuration = builder.Configuration;

builder.Services.AddDbContext<CandleDataBaseContext>(p =>
    p.UseSqlServer(Configuration["Kasbotech_Candle_Connection"]), ServiceLifetime.Singleton);

var app = builder.Build();



app.Run();