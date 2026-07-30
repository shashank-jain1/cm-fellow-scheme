using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.SendOtp;

public sealed record SendOtpCommand : ICommand<Result>
{
    public int ApplicantId { get; init; }
    public string EmailId { get; init; } = null!;
}
