using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace CmScheme.Tests.Smoke;

public class ModuleEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ModuleEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Application_HasEndpointDataSource()
    {
        // Arrange & Act
        using var scope = _factory.Services.CreateScope();
        var endpointDataSource = scope.ServiceProvider.GetRequiredService<IEnumerable<EndpointDataSource>>();

        // Assert
        Assert.NotNull(endpointDataSource);
        Assert.NotEmpty(endpointDataSource);
    }

    [Fact]
    public void Application_RegistersHealthCheckEndpoint()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var endpointDataSource = scope.ServiceProvider.GetRequiredService<IEnumerable<EndpointDataSource>>();

        // Act
        var allEndpoints = endpointDataSource
            .SelectMany(es => es.Endpoints)
            .OfType<RouteEndpoint>()
            .ToList();

        var healthEndpoint = allEndpoints.FirstOrDefault(e =>
            e.RoutePattern.RawText?.Contains("health") == true);

        // Assert
        Assert.NotNull(healthEndpoint);
    }

    [Fact]
    public void Application_RegistersApiEndpoints()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var endpointDataSource = scope.ServiceProvider.GetRequiredService<IEnumerable<EndpointDataSource>>();

        // Act
        var allEndpoints = endpointDataSource
            .SelectMany(es => es.Endpoints)
            .OfType<RouteEndpoint>()
            .ToList();

        var apiEndpoints = allEndpoints.Where(e =>
            e.RoutePattern.RawText?.Contains("/api/") == true).ToList();

        // Assert
        Assert.NotEmpty(apiEndpoints);
    }

    [Fact]
    public void Application_HasMultipleEndpointDataSources()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var endpointDataSource = scope.ServiceProvider.GetRequiredService<IEnumerable<EndpointDataSource>>();

        // Act
        var allEndpoints = endpointDataSource
            .SelectMany(es => es.Endpoints)
            .OfType<RouteEndpoint>()
            .ToList();

        // Assert - multiple route endpoints registered
        Assert.True(allEndpoints.Count > 5, $"Expected multiple route endpoints but found {allEndpoints.Count}");
    }
}