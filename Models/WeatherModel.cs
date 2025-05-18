namespace WeatherApp.Models;

public class WeatherModel
{
    public string City { get; set; }
    public string WeatherText { get; set; }
    public string Temperature { get; set; }
    public string IconUrl { get; set; }
    public List<ForecastDay> Forecasts { get; set; }
}