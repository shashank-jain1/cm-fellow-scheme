using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.RejectRegistration;

public sealed record RejectRegistrationCommand : ICommand<Result>
{
    public int ApplicantId { get; init; }
    public string Reason { get; init; } = null!;
    public int RejectedBy { get; init; }
}
