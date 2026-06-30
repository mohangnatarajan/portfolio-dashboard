using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HoldingsController : ControllerBase
{
    private readonly KiteService _kiteService;

    public HoldingsController(KiteService kiteService)
    {
        _kiteService = kiteService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var holdings = _kiteService.GetHoldings();
            return Ok(holdings);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
