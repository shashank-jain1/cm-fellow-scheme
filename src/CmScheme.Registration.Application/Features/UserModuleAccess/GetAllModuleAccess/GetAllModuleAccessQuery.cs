using Ardalis.Result;
using Mediator;
using CmScheme.Registration.Core.Dtos;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GetAllModuleAccess;

public sealed record GetAllModuleAccessQuery : IQuery<Result<List<UserAccessSummaryDto>>>
{
    public bool? IsActive { get; init; }
}
