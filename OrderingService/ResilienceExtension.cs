using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace OrderingService;

public static class ResilienceExtension
{
    public static IServiceCollection AddResilience(this IServiceCollection services)
    {
        // Define Polly policies
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .RetryAsync(3);

        var circuitBreakerPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(5);

        var bulkheadPolicy = Policy.BulkheadAsync<HttpResponseMessage>(10, 20);

        var fallbackPolicy = Policy<HttpResponseMessage>
            .Handle<Exception>()
            .FallbackAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Fallback response")
            });


        // Wireup defined policies with the HttpClient.
        services.AddHttpClient("InventoryClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/inventory"); // Replace with actual URL
            })
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(circuitBreakerPolicy)
            .AddPolicyHandler(timeoutPolicy)
            .AddPolicyHandler(bulkheadPolicy)
            .AddPolicyHandler(fallbackPolicy);

        return services;
    }
}