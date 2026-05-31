using Api.Commands;
using Api.Dto;
using Api.Handlers;
using Api.Querys;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherCommand command)
        {
            WeatherHandler _weatherHandler = new WeatherHandler();
            _weatherHandler.Handle(command);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            WeatherQueryHandler _weatherQueryHandler = new WeatherQueryHandler();
            return _weatherQueryHandler.Handle();
        }

    }
}
