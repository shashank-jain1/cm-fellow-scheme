using Ardalis.Result;
using Mediator;
using CmScheme.Registration.Application.Features.UserModuleAccess.Dtos;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.ListModules;

public sealed record ListModulesQuery : IQuery<Result<List<ModuleMasterDto>>>
{
    public bool? IsActive { get; init; }
}
