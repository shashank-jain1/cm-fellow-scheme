using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.SendOtp;

/// <summary>
/// Mobile OTP is issued before the registration form is submitted, so there is no
/// applicant record yet. The mobile number is the identity being proven, and it is the
/// key the OTP is stored under — VerifyMobileOtp must look it up by the same key.
/// </summary>
public sealed record SendOtpCommand : ICommand<Result>
{
    public string MobileNumber { get; init; } = null!;
}
