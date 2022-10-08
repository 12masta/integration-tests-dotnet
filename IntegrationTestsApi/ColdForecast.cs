namespace IntegrationTestsApi;

public class ColdForecast : IWeatherForecast
{
    public WeatherForecast GetPrediction()
    {
        return new WeatherForecast()
        {
            Date = DateTime.UtcNow,
            TemperatureC = -20,
            Summary = "Low temperature is expected."
        };
    }
}