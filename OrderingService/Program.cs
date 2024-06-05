// OrderingService/Program.cs

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync().ConfigureAwait(false);