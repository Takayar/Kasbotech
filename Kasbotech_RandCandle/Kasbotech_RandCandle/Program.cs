using Kasbotech_RandCandle.Infrastructure.Context;
using Kasbotech_RandCandle.Model.Services;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using ServiceLifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHostedService<CandleRandService>();

IConfiguration Configuration = builder.Configuration;
builder.Services.AddDbContext<CandleDataBaseContext>(p =>
    p.UseSqlServer(Configuration["Kasbotech_Candle_Connection"]), ServiceLifetime.Singleton);



Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(Configuration)
    .CreateLogger();
builder.Host.UseSerilog();





var app = builder.Build();
app.Run();

