using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.RevokeModuleAccess;

public sealed record RevokeModuleAccessCommand : ICommand<Result<bool>>
{
    public int UserModuleAccessId { get; init; }
    public int PerformedBy { get; init; }
}
