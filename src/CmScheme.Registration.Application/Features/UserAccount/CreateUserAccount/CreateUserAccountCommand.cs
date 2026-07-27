using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;

public sealed record CreateUserAccountCommand : ICommand<Result<int>>
{
    public int ApplicantId { get; init; }
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Role { get; init; } = null!;
    public int CreatedBy { get; init; }
}
