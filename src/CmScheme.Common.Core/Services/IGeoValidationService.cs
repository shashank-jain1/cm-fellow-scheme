namespace CmScheme.Common.Core.Services;

public interface IGeoValidationService
{
    Task<bool> IsWithinAssignedAreaAsync(
        int userAccountId,
        decimal latitude,
        decimal longitude,
        decimal maxDistanceKm = 5.0m,
        CancellationToken cancellationToken = default);
}
