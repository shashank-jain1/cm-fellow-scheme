using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.AssignRole;

public sealed record AssignRoleCommand : ICommand<Result>
{
    public int UserAccountId { get; init; }
    public string Role { get; init; } = null!;
    public int ModifiedBy { get; init; }
}
