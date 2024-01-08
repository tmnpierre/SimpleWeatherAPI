using Microsoft.AspNetCore.Mvc;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather()
        {
            var apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
            string url = $"https://api.openweathermap.org/data/2.5/weather?q=Paris&appid={apiKey}";

            using (var client = new HttpClient())
            {
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
}