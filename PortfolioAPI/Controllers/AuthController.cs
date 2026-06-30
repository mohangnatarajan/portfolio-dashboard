using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly KiteService _kiteService;

    public AuthController(KiteService kiteService)
    {
        _kiteService = kiteService;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Redirect(_kiteService.GetLoginUrl());
    }

    [HttpGet("callback")]
    public IActionResult Callback([FromQuery] string request_token, [FromQuery] string status)
    {
        if (status != "success" || string.IsNullOrEmpty(request_token))
            return BadRequest(new { message = "Kite login failed or request token missing." });

        try
        {
            var accessToken = _kiteService.ExchangeToken(request_token);
            return Ok(new { message = "Authentication successful. You can now use the API.", accessToken });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Token exchange failed.", error = ex.Message });
        }
    }
}
