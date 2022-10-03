using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace ApiTests;

public class IntegrationTests
{
    [Test]
    public async Task InitialIntegrationTest()
    {
        var url = "/WeatherForecast";
        var sut = new WebApplicationFactory<Program>();
        var client = sut.CreateClient();
        
        var response = await client.GetAsync(url);

        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
    }
}