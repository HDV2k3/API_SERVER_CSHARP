using Microsoft.AspNetCore.Mvc;

namespace lab1_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Bảo vệ logger khỏi null
        }

        /// <summary>
        /// Lấy danh sách thời tiết
        /// </summary>
        /// <returns>Danh sách thời tiết</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("WeatherForecast endpoint called.");

            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)] // Lấy ngẫu nhiên Summary từ Summaries
            })
            .ToArray();

            _logger.LogInformation("WeatherForecast data generated: {@Forecasts}", forecasts);
            return forecasts;
        }
    }
}
