// OrderingService/Program.cs

using System.Net;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddHttpClient("WeatherClient", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5000/weather"); // Replace with actual URL
    })
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreakerPolicy)
    .AddPolicyHandler(timeoutPolicy)
    .AddPolicyHandler(bulkheadPolicy)
    .AddPolicyHandler(fallbackPolicy);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();