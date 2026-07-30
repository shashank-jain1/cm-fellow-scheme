namespace CmScheme.Common.Core.Services;

public interface IOtpService
{
    Task<string> GenerateOtpAsync(string identifier, CancellationToken ct = default);
    Task<bool> VerifyOtpAsync(string identifier, string otp, CancellationToken ct = default);
}
