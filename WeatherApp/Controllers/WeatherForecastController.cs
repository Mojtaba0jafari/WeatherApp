using Microsoft.AspNetCore.Mvc;
using WeatherApi.Services;
using WeatherApp.Models;

namespace WeatherApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _service;

    public WeatherController(WeatherService service)
    {
        _service = service;
    }

    /// <summary>Get a mock forecast for a city.</summary>
    [HttpGet("{city}")]
    public ActionResult<WeatherForecast> Get(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest("City name is required.");

        return Ok(_service.GetForecast(city));
    }
}
