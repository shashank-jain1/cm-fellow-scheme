namespace CmScheme.Common.Core.Services;

public interface IGeoValidationService
{
    Task<bool> IsWithinAssignedAreaAsync(
        int userAccountId,
        decimal currentLatitude,
        decimal currentLongitude,
        decimal targetLatitude,
        decimal targetLongitude,
        decimal maxDistanceKm = 5.0m,
        CancellationToken cancellationToken = default);
}
