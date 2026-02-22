using Newtonsoft.Json;

public class WeatherProviderService : IWeatherProvider
{
    public async Task<WeatherResponse> GetWeather(string city)
    {
        var apiKey = "YOUR_OPENWEATHER_KEY";

        using var client = new HttpClient();

        var response = await client.GetStringAsync(
            $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric");

        dynamic data = JsonConvert.DeserializeObject(response);

        return new WeatherResponse
        {
            City = city,
            Temperature = data.main.temp,
            Description = data.weather[0].description,
            Latitude = data.coord.lat,
            Longitude = data.coord.lon
        };
    }
}