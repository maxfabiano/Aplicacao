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
            _weatherHandler.Handle(command);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherQueryHandler.Handle();
        }

    }
