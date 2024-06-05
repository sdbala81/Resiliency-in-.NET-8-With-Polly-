// OrderingService/Controllers/WeatherController.cs

using Microsoft.AspNetCore.Mvc;

namespace OrderingService.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class DepartmentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DepartmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("test-retry")]
        public async Task<IActionResult> TestRetry()
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }

        [HttpGet("test-circuitbreaker")]
        public async Task<IActionResult> TestCircuitBreaker()
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }

        [HttpGet("test-timeout")]
        public async Task<IActionResult> TestTimeout()
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }

        [HttpGet("test-bulkhead")]
        public async Task<IActionResult> TestBulkhead()
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }

        [HttpGet("test-fallback")]
        public async Task<IActionResult> TestFallback()
        {
            var client = _httpClientFactory.CreateClient("WeatherClient");
            var response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }

            return StatusCode((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }
    }
}
