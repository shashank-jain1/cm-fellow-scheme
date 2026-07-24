using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.ListRegistrations;

public sealed record ListRegistrationsQuery(string? SearchTerm, string? Status, int PageNumber, int PageSize)
    : IQuery<Result<List<Core.Dtos.RegistrationListItem>>>;
