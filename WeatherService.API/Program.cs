var builder = WebApplication.CreateBuilder(args);

/* ✅ Swagger */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWeatherProvider, WeatherProviderService>();
var app = builder.Build();

/* ✅ Middleware Pipeline */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
app.UseHttpsRedirection();

/* ✅ Weather Forecast Endpoint */
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing","Bracing","Chilly","Cool","Mild",
        "Warm","Balmy","Hot","Sweltering","Scorching"
    };

    return Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ));
});

app.Run();

/* ✅ Record Model (Important — Must be inside same namespace/file) */
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}