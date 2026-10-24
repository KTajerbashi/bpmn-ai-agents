using Microsoft.AspNetCore.Mvc;

namespace BPMN.Ai.Agents.Services.Identity.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            List<WeatherForecast> data = new();
            foreach (var item in Enumerable.Range(1, 60))
            {
                await Task.Delay(1000);
                data.Add(new() { Date = DateOnly.MinValue, Summary = $"{DateTime.Now.Hour} - {DateTime.Now.Minute} - {DateTime.Now.Second}", TemperatureC = DateTime.Now.Second });
            }
            return data.ToList();
        }
    }
}
