using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.ApproveRegistration;

public sealed record ApproveRegistrationCommand : ICommand<Result>
{
    public int ApplicantId { get; init; }
    public int ApprovedBy { get; init; }
}
