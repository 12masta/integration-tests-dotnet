using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTestsApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ApiTests;

public class IntegrationTests
{
    private readonly string _url = "/WeatherForecast";

    [Test]
    public async Task InitialIntegrationTest()
    {
        var sut = new WebApplicationFactory<Program>();
        var client = sut.CreateClient();

        var response = await client.GetAsync(_url);

        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
    }

    [Test]
    public async Task CheckIfProperValuesAreReturned()
    {
        var sut = new WebApplicationFactory<Program>();
        var client = sut.CreateClient();

        var result = await client.GetFromJsonAsync<WeatherForecast>(_url);

        result.Should().NotBeNull();
        result!.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        result.Summary.Should().Be("High temperature is expected.");
        result.TemperatureC.Should().Be(30);
    }

    [Test]
    public async Task CheckIfAlternativeImplementationWorksWell()
    {
        var sut = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(testServices =>
                    testServices.AddTransient<IWeatherForecast, ColdForecast>()));
        var client = sut.CreateClient();

        var result = await client.GetFromJsonAsync<WeatherForecast>(_url);

        result.Should().NotBeNull();
        result!.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        result.Summary.Should().Be("Low temperature is expected.");
        result.TemperatureC.Should().Be(-20);
    }
}