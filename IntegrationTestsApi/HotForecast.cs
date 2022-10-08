namespace IntegrationTestsApi;

public class HotForecast : IWeatherForecast
{
    public WeatherForecast GetPrediction()
    {
        return new WeatherForecast()
        {
            Date = DateTime.UtcNow,
            TemperatureC = 30,
            Summary = "High temperature is expected."
        };
    }
}