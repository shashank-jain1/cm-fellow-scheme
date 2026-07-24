using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.DeactivateUserAccount;

public sealed record DeactivateUserAccountCommand(int UserAccountId)
    : ICommand<Result>;
