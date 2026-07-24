using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.GetRegistrationById;

public sealed record GetRegistrationByIdQuery(int ApplicantId)
    : IQuery<Result<Core.Dtos.ApplicantDto>>;
