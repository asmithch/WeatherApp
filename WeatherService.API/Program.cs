using WeatherService.Application.Services;

var builder = WebApplication.CreateBuilder(args);

/* ================= Services ================= */
builder.Services.AddScoped<IWeatherProvider, WeatherProviderServices>();

/* ================= Redis Cache ================= */
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

/* ================= Swagger ================= */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* ================= Controllers ================= */
builder.Services.AddControllers();

var app = builder.Build();

/* ================= Middleware ================= */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();