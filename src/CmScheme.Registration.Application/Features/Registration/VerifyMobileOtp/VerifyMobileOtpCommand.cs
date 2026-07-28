using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.VerifyMobileOtp;

public sealed record VerifyMobileOtpCommand : ICommand<Result>
{
    public int ApplicantId { get; init; }
    public string MobileNumber { get; init; } = null!;
    public string OtpCode { get; init; } = null!;
}
