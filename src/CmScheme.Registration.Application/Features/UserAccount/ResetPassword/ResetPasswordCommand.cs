using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.ResetPassword;

public sealed record ResetPasswordCommand : ICommand<Result>
{
    /// <summary>
    /// Single-use token issued by ForgotPassword and delivered by email. This — not the
    /// email address — is what proves the caller owns the account.
    /// </summary>
    public string Token { get; init; } = null!;

    public string NewPassword { get; init; } = null!;
}
