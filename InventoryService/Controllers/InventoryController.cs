using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class InventoryController : Controller
{
    private int _requestCount;


    [HttpGet("retry")]
    public async Task<IActionResult> Retry()
    {
        await Task.Delay(1000); // simulate some data processing by delaying for 100 milliseconds

        _requestCount++;

        return _requestCount % 4 == 0
            ? // only one of out four requests will succeed
            Ok()
            : StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong");
    }

    [HttpGet("circuitbreaker")]
    public async Task<IActionResult> CircuitBreaker()
    {
        // Simulate a circuit breaker scenario
        // You can replace this with your own logic
        await Task.Delay(1000);
        return StatusCode((int)HttpStatusCode.InternalServerError, "Circuit breaker triggered");
    }

    [HttpGet("timeout")]
    public async Task<IActionResult> Timeout()
    {
        // Simulate a timeout scenario
        // You can replace this with your own logic
        await Task.Delay(5000);
        return Ok("Timeout scenario");
    }

    [HttpGet("bulkhead")]
    public async Task<IActionResult> Bulkhead()
    {
        // Simulate a bulkhead scenario
        // You can replace this with your own logic
        await Task.Delay(1000);
        return Ok("Bulkhead scenario");
    }

    [HttpGet("fallback")]
    public async Task<IActionResult> Fallback()
    {
        // Simulate a fallback scenario
        // You can replace this with your own logic
        await Task.Delay(1000);
        return Ok("Fallback scenario");
    }
}