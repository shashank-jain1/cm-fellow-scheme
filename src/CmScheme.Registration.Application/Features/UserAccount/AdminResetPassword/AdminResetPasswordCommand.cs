using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.AdminResetPassword;

/// <summary>
/// Administrative password reset: an authorised administrator sets a user's password
/// directly. Distinct from the self-service token flow in <c>ResetPassword</c>.
/// </summary>
public sealed record AdminResetPasswordCommand : ICommand<Result>
{
    public int UserAccountId { get; init; }
    public string NewPassword { get; init; } = null!;
}
