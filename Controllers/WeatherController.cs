using Microsoft.AspNetCore.Mvc;

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
            var apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
            string url = $"https://api.openweathermap.org/data/2.5/weather?q=Paris&appid={apiKey}";

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return Ok(jsonData);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error calling the weather service");
            }
        }
    }
}
