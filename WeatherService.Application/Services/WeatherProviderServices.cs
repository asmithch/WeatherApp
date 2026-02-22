using WeatherService.Domain;
using Newtonsoft.Json;

namespace WeatherService.Application.Services;

public class WeatherProviderServices : IWeatherProvider
{
    public async Task<WeatherResponse> GetWeather(string city)
    {
        var apiKey = "YOUR_KEY";

        using var client = new HttpClient();

        var response = await client.GetStringAsync(
            $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}");

        return JsonConvert.DeserializeObject<WeatherResponse>(response)!;
    }
}