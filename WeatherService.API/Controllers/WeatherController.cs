using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WeatherService.Application.Services;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherProvider _weatherProvider;
    private readonly IDistributedCache _cache;

    public WeatherController(
        IWeatherProvider weatherProvider,
        IDistributedCache cache)
    {
        _weatherProvider = weatherProvider;
        _cache = cache;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        // ⭐ Check Redis Cache First
        var cachedData = await _cache.GetStringAsync(city);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return Ok(JsonConvert.DeserializeObject(cachedData));
        }

        // ⭐ If Not Found → Call Weather API
        var weather = await _weatherProvider.GetWeather(city);

        // ⭐ Store in Redis Cache
        await _cache.SetStringAsync(
            city,
            JsonConvert.SerializeObject(weather));

        return Ok(weather);
    }
}