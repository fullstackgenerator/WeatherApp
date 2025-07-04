﻿using System.Globalization;
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

    public async Task<WeatherModel> GetCurrentConditionsAsync(string city)
    {
        var url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}";
        var response = await _httpClient.GetStringAsync(url);
        dynamic data = JsonConvert.DeserializeObject(response);

        return new WeatherModel
        {
            City = data.location.name,
            WeatherText = data.current.condition.text,
            Temperature = data.current.temp_c + " °C",
            IconUrl = "https:" + data.current.condition.icon
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
                MaxTemp = day.day.maxtemp_c + " °C",
                IconUrl = "https:" + day.day.condition.icon
            });
        }

        return forecastList;
    }
}