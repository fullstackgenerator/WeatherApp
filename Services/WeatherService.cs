using System.Globalization;
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
        _apiKey = configuration["WeatherApi:ApiKey"];
    }

    public async Task<string?> GetLocationKeyAsync(string city)
    {
        var url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        dynamic data = JsonConvert.DeserializeObject(content);
        
        return (string)data.location.name;
    }

    public async Task<WeatherModel> GetCurrentConditionsAsync(string city)
    {
        var url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}";
        var response = await _httpClient.GetStringAsync(url);
        dynamic data = JsonConvert.DeserializeObject(response);

        return new WeatherModel
        {
            City = data.location.name,
            WeatherText = data.current.condition.text,
            Temperature = data.current.temp_c + " °C"
        };
    }

    public async Task<List<ForecastDay>> GetFiveDayForecastAsync(string city)
    {
        var url = $"http://api.weatherapi.com/v1/forecast.json?key={_apiKey}&q={city}&days=5";
        var response = await _httpClient.GetStringAsync(url);
        dynamic data = JsonConvert.DeserializeObject(response);

        var forecastList = new List<ForecastDay>();

        foreach (var day in data.forecast.forecastday)
        {
            forecastList.Add(new ForecastDay
            {
                Date = DateTime.Parse((string)day.date).ToString("yyyy-MM-dd"),
                IconPhrase = day.day.condition.text,
                MinTemp = day.day.mintemp_c + " °C",
                MaxTemp = day.day.maxtemp_c + " °C"
            });
        }

        return forecastList;
    }

}