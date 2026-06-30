namespace PortfolioAPI.Services;

public class TokenStore
{
    public string? AccessToken { get; set; }
    public bool HasToken => !string.IsNullOrEmpty(AccessToken);
}
