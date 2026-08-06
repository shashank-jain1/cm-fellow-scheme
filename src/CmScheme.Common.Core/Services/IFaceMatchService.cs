namespace CmScheme.Common.Core.Services;

public interface IFaceMatchService
{
    Task<(decimal MatchPercentage, string VerificationStatus)> CompareFacesAsync(string selfieBase64, string referencePhotoPath, CancellationToken cancellationToken = default);
}
