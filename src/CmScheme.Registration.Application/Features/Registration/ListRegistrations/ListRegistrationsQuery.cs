using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.ListRegistrations;

public sealed record ListRegistrationsQuery : IQuery<Result<List<Core.Dtos.RegistrationListItem>>>
{
    public string? SearchTerm { get; init; }
    public string? Status { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
