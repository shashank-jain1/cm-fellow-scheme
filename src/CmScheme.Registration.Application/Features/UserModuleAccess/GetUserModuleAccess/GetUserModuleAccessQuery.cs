using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GetUserModuleAccess;

public sealed record GetUserModuleAccessQuery : IQuery<Result<List<Dtos.ModuleAccessDto>>>
{
    public int UserAccountId { get; init; }
}
