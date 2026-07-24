using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.AssignRole;

public sealed record AssignRoleCommand(int UserAccountId, string Role, int ModifiedBy)
    : ICommand<Result>;
