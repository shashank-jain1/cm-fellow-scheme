using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GetAllModuleAccess;

public sealed record GetAllModuleAccessQuery : IQuery<Result<List<Dtos.UserAccessSummaryDto>>>
{
    public bool? IsActive { get; init; }
}
