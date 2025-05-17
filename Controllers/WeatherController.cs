using WeatherApp.Services;

namespace WeatherApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class WeatherController : Controller
{
    private readonly WeatherService _weatherService;

    public WeatherController(IConfiguration configuration)
    {
        _weatherService = new WeatherService(configuration);
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Index(string city)
    {
        try
        {
            var weather = await _weatherService.GetCurrentConditionsAsync(city);
            var forecast = await _weatherService.GetFiveDayForecastAsync(city);
            weather.Forecasts = forecast;
            return View(weather);
        }
        catch
        {
            ViewBag.Error = "City not found or API failed.";
            return View();
        }
    }
}