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
    public async Task<IActionResult> Index(string city)
    {
        var location = await _weatherService.GetLocationKeyAsync(city);
        if (location == null)
        {
            ViewBag.Error = "City not found";
            return View();
        }
        var weather = await _weatherService.GetCurrentConditionsAsync(location.Key, location.LocalizedName);
        return View(weather);
    }
}