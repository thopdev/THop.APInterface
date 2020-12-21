using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace THop.APInterface.API.Controllers
{

    public class GenerateControllerAttribute : Attribute
    {
        public GenerateControllerAttribute()
        {
            
        }

        public GenerateControllerAttribute(string route)
        {

        }
    }
    [GenerateController("ParameterValue")]

    public interface IWeatherForecastController
    {
        [HttpGet("Test")]
        string Get([FromQuery] WeatherForecastController text, string test);
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase, IWeatherForecastController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get(WeatherForecastController text, string test)
        {
            var rng = new Random();
            return "";
        }
    }
}
