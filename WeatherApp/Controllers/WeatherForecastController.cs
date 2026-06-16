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

    /// <summary>Get a live forecast for a city.</summary>
    [HttpGet("{city}")]
    public async Task<ActionResult<WeatherForecast>> Get(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest("City name is required.");

        var forecast = await _service.GetForecastAsync(city);

        if (forecast == null)
            return NotFound($"City '{city}' not found or API error.");

        return Ok(forecast);
    }
}
