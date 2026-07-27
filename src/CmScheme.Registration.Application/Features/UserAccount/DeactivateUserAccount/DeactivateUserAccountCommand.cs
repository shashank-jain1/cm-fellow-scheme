using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.DeactivateUserAccount;

public sealed record DeactivateUserAccountCommand : ICommand<Result>
{
    public int UserAccountId { get; init; }
}
