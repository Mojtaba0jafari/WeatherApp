using WeatherApp.Models;

namespace WeatherApi.Services
{
    public class WeatherService
    {
        private readonly string[] _summaries = new[]
        {
            "Freezing", "Cool", "Mild", "Warm", "Hot"
        };

        public WeatherForecast GetForecast(string city)
        {
            var random = new Random();
            int temperatureC = random.Next(-10, 40);

            int index = temperatureC switch
            {
                < 0 => 0,  // Freezing
                < 10 => 1,  // Cool
                < 20 => 2,  // Mild
                < 30 => 3,  // Warm
                _ => 4   // Hot
            };

            return new WeatherForecast
            {
                City = city,
                Date = DateTime.Now,
                TemperatureC = temperatureC,
                Summary = _summaries[index]
            };
        }
    }
}
