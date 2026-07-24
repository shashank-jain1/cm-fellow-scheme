using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.ApproveRegistration;

public sealed record ApproveRegistrationCommand(int ApplicantId, int ApprovedBy)
    : ICommand<Result>;
