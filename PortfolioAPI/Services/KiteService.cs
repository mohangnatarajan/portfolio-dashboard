using PortfolioAPI.Models;

namespace PortfolioAPI.Services;

public class KiteService
{
    private readonly string _apiKey;
    private readonly string _apiSecret;
    private readonly TokenStore _tokenStore;

    public KiteService(IConfiguration configuration, TokenStore tokenStore)
    {
        _apiKey = configuration["Kite:ApiKey"]
            ?? throw new InvalidOperationException("Kite:ApiKey is not configured.");
        _apiSecret = configuration["Kite:ApiSecret"]
            ?? throw new InvalidOperationException("Kite:ApiSecret is not configured.");
        _tokenStore = tokenStore;
    }

    public string GetLoginUrl() =>
        $"https://kite.trade/connect/login?api_key={_apiKey}&v=3";

    public string ExchangeToken(string requestToken)
    {
        var kite = new KiteConnect.Kite(APIKey: _apiKey);
        var session = kite.GenerateSession(requestToken, _apiSecret);
        _tokenStore.AccessToken = session.AccessToken;
        return session.AccessToken;
    }

    public List<Holding> GetHoldings()
    {
        if (!_tokenStore.HasToken)
            throw new InvalidOperationException("Not authenticated. Visit /api/auth/login first.");

        var kite = new KiteConnect.Kite(APIKey: _apiKey, AccessToken: _tokenStore.AccessToken);
        var kiteHoldings = kite.GetHoldings();

        return kiteHoldings.Select(h => new Holding
        {
            Symbol = h.TradingSymbol,
            Quantity = h.Quantity,
            AveragePrice = h.AveragePrice,
            LastPrice = h.LastPrice,
            Pnl = h.PNL
        }).ToList();
    }
}
