public class WeatherResponse
{
    public string City { get; set; }=string.Empty;
    public double Temperature { get; set; }
    public string Description { get; set; }=string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}