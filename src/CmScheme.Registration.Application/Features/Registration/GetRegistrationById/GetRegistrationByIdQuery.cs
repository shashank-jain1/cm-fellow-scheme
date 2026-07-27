using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.GetRegistrationById;

public sealed record GetRegistrationByIdQuery : IQuery<Result<Core.Dtos.ApplicantDto>>
{
    public int ApplicantId { get; init; }
}
