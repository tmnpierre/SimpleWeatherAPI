using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherService.Controllers
{
    public class WeatherController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public WeatherController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather()
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
                string url = $"https://api.openweathermap.org/data/2.5/weather?q=Paris&appid={apiKey}";

                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var externalWeatherData = JsonConvert.DeserializeObject<ExternalWeatherApiResponse>(jsonData);
                    var weatherResponse = new WeatherResponse
                    {
                        City = externalWeatherData.Name,
                        Temperature = externalWeatherData.Main.Temp,
                        Description = externalWeatherData.Weather[0].Description
                    };
                    return Ok(weatherResponse);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error from weather service");
                }
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service unavailable");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class ExternalWeatherApiResponse
    {
        public string Name { get; set; }
        public Main Main { get; set; }
        public Weather[] Weather { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }
}