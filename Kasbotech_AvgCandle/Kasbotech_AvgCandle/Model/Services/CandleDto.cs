namespace Kasbotech_AvgCandle.Model.Services;

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