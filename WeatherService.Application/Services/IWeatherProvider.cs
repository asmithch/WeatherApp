public interface IWeatherProvider
{
    Task<WeatherResponse> GetWeather(string city);
}