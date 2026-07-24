using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;

public sealed record CreateUserAccountCommand(
    int ApplicantId,
    string Username,
    string Password,
    string Role,
    int CreatedBy)     : ICommand<Result<int>>;
