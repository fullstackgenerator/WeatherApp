using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["AccuWeather:ApiKey"];
    }

    public async Task<LocationResult> GetLocationKeyAsync(string city)
    {
        var url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={_apiKey}&q={city}";
        var response = await _httpClient.GetStringAsync(url);
        var locations = JsonConvert.DeserializeObject<List<LocationResult>>(response);
        return locations?.FirstOrDefault();
    }

    public async Task<WeatherModel> GetCurrentConditionsAsync(string locationKey, string cityName)
    {
        var url = $"http://dataservice.accuweather.com/currentconditions/v1/{locationKey}?apikey={_apiKey}";
        var response = await _httpClient.GetStringAsync(url);
        dynamic data = JsonConvert.DeserializeObject(response);

        return new WeatherModel
        {
            City = cityName,
            WeatherText = data[0].WeatherText,
            Temperature = data[0].Temperature.Metric.Value + " °C"
        };
    }
}