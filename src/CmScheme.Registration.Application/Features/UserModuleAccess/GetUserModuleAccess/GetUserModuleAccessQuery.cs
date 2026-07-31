using Ardalis.Result;
using Mediator;
using CmScheme.Registration.Core.Dtos;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GetUserModuleAccess;

public sealed record GetUserModuleAccessQuery : IQuery<Result<List<ModuleAccessDto>>>
{
    public int UserAccountId { get; init; }
}
