using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHttpLogging(o =>
{
    o.LoggingFields = HttpLoggingFields.RequestHeaders;
    o.CombineLogs = true;
    o.RequestHeaders.Add("Hello");
    o.RequestHeaders.Add("traceparent");
    o.RequestHeaders.Add("tracestate");
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();
app.UseCors();
app.MapControllers();
app.UseHttpLogging();

app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));


//app.UseRateLimiter();

app.Run();
