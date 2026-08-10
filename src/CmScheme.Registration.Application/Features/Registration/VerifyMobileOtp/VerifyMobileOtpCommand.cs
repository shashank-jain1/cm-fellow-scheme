using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;

/// <summary>
/// Verified against the OTP stored under the same mobile number by SendOtp. Runs before
/// the form is submitted, so no applicant record is required.
/// </summary>
public sealed record VerifyMobileOtpCommand : ICommand<Result>
{
    public string MobileNumber { get; init; } = null!;
    public string OtpCode { get; init; } = null!;
}
