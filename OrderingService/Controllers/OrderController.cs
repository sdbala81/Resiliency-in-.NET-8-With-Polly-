// OrderingService/Controllers/WeatherController.cs

using Microsoft.AspNetCore.Mvc;

namespace OrderingService.Controllers;

[ApiController]
[Route("[controller]s")]
public class OrderController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    [HttpGet("retry")]
    public async Task<IActionResult> TestRetry()
    {
        var client = httpClientFactory.CreateClient("InventoryClient");
        var response = await client.GetAsync("retry");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
    }

    [HttpGet("circuitbreaker")]
    public async Task<IActionResult> TestCircuitBreaker()
    {
        var client = httpClientFactory.CreateClient("InventoryClient");
        var response = await client.GetAsync("circuitbreaker");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
    }

    [HttpGet("timeout")]
    public async Task<IActionResult> TestTimeout()
    {
        var client = httpClientFactory.CreateClient("InventoryClient");
        var response = await client.GetAsync("timeout");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
    }

    [HttpGet("bulkhead")]
    public async Task<IActionResult> TestBulkhead()
    {
        var client = httpClientFactory.CreateClient("InventoryClient");
        var response = await client.GetAsync("timeout");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
    }

    [HttpGet("fallback")]
    public async Task<IActionResult> TestFallback()
    {
        var client = httpClientFactory.CreateClient("InventoryClient");
        var response = await client.GetAsync("timeout");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
    }
}