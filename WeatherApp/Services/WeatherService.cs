using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApi.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApi:ApiKey"]
                ?? throw new InvalidOperationException("WeatherApi:ApiKey is not configured.");
            _baseUrl = configuration["WeatherApi:BaseUrl"]
                ?? throw new InvalidOperationException("WeatherApi:BaseUrl is not configured.");
        }

        public async Task<WeatherForecast?> GetForecastAsync(string city)
        {
            var url = $"{_baseUrl}?q={Uri.EscapeDataString(city)}&appid={_apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var temperatureC = root.GetProperty("main").GetProperty("temp").GetDouble();
            var summary = root.GetProperty("weather")[0].GetProperty("description").GetString();
            var name = root.GetProperty("name").GetString();

            return new WeatherForecast
            {
                City = name ?? city,
                Date = DateTime.Now,
                TemperatureC = (int)Math.Round(temperatureC),
                Summary = summary ?? string.Empty
            };
        }
    }
}
