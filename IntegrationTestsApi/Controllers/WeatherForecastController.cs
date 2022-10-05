using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecast _forecast;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecast forecast)
    {
        _logger = logger;
        _forecast = forecast;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public WeatherForecast Get()
    {
        _logger.LogInformation("Using IWeatherForecast implementation: {0}", _forecast.ToString());
        return _forecast.GetPrediction();
    }
}