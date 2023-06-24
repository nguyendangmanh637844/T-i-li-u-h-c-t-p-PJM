using Microsoft.AspNetCore.Mvc;
using SendEmail.Models;
using SendEmail.Service;

namespace SendEmail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SendMailService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, SendMailService sendMailService)
        {
            _logger = logger;
            _service = sendMailService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public List<Subject> Get()
        {
            return _service.getAll();
        }
    }
}