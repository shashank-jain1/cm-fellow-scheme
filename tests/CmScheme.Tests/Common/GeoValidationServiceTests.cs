using CmScheme.Common.Infrastructure.Services;

namespace CmScheme.Tests.Common;

public class GeoValidationServiceTests
{
    private readonly GeoValidationService _service = new();

    [Fact]
    public async Task IsWithinAssignedAreaAsync_Zero_Current_Coordinates_Always_True()
    {
        bool result = await _service.IsWithinAssignedAreaAsync(1, 0m, 0m, 12.97m, 77.59m);
        Assert.True(result);
    }

    [Fact]
    public async Task IsWithinAssignedAreaAsync_Identical_Coordinates_Is_True()
    {
        bool result = await _service.IsWithinAssignedAreaAsync(1, 12.9716m, 77.5946m, 12.9716m, 77.5946m);
        Assert.True(result);
    }

    [Fact]
    public async Task IsWithinAssignedAreaAsync_Same_City_Within_Default_5Km_Is_True()
    {
        bool result = await _service.IsWithinAssignedAreaAsync(1, 12.9716m, 77.5946m, 12.9768m, 77.6070m);
        Assert.True(result);
    }

    [Fact]
    public async Task IsWithinAssignedAreaAsync_Far_Away_Is_False()
    {
        bool result = await _service.IsWithinAssignedAreaAsync(1, 12.9716m, 77.5946m, 28.6139m, 77.2090m);
        Assert.False(result);
    }

    [Fact]
    public async Task IsWithinAssignedAreaAsync_Expanded_MaxDistance_Allows_Distant_Target()
    {
        bool result = await _service.IsWithinAssignedAreaAsync(1, 12.9716m, 77.5946m, 28.6139m, 77.2090m, 2000m);
        Assert.True(result);
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(2)]
    [InlineData(4.9)]
    public async Task IsWithinAssignedAreaAsync_Distances_Under_Radius_Are_True(decimal maxDistanceKm)
    {
        bool result = await _service.IsWithinAssignedAreaAsync(
            1, 12.9716m, 77.5946m, 12.9750m, 77.5946m, maxDistanceKm);
        Assert.True(result);
    }

    [Theory]
    [InlineData(0.1)]
    [InlineData(0.05)]
    public async Task IsWithinAssignedAreaAsync_Tiny_Radius_Rejects_Moderate_Distance(decimal maxDistanceKm)
    {
        bool result = await _service.IsWithinAssignedAreaAsync(
            1, 12.9716m, 77.5946m, 12.9750m, 77.6000m, maxDistanceKm);
        Assert.False(result);
    }

    [Fact]
    public async Task IsWithinAssignedAreaAsync_Supports_Cancellation_Token()
    {
        using var cts = new CancellationTokenSource();
        bool result = await _service.IsWithinAssignedAreaAsync(
            1, 12.9716m, 77.5946m, 12.9716m, 77.5946m, 5m, cts.Token);
        Assert.True(result);
    }
}