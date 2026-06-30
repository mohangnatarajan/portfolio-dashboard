namespace PortfolioAPI.Models;

public class Holding
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal AveragePrice { get; set; }
    public decimal LastPrice { get; set; }
    public decimal Pnl { get; set; }
}
