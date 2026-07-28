using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.ResetPassword;

public sealed record ResetPasswordCommand : ICommand<Result>
{
    public string Email { get; init; } = null!;
    public string NewPassword { get; init; } = null!;
}
