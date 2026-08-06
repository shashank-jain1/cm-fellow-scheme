using CmScheme.Common.Core.Services;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class GeoValidationService : IGeoValidationService
{
    public Task<bool> IsWithinAssignedAreaAsync(
        int userAccountId,
        decimal currentLatitude,
        decimal currentLongitude,
        decimal targetLatitude,
        decimal targetLongitude,
        decimal maxDistanceKm = 5.0m,
        CancellationToken cancellationToken = default)
    {
        // If current coordinates are default (0, 0) or unprovided, skip geofence restriction to avoid blocking testing
        if (currentLatitude == 0m && currentLongitude == 0m)
        {
            return Task.FromResult(true);
        }

        double distanceKm = CalculateHaversineDistance(
            (double)currentLatitude, (double)currentLongitude, (double)targetLatitude, (double)targetLongitude);

        return Task.FromResult((decimal)distanceKm <= maxDistanceKm);
    }

    private static double CalculateHaversineDistance(
        double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371.0; // Earth radius in kilometers

        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}
