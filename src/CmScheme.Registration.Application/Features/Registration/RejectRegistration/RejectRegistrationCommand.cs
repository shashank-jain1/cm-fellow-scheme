using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.RejectRegistration;

public sealed record RejectRegistrationCommand(int ApplicantId, string Reason, int RejectedBy)
    : ICommand<Result>;
