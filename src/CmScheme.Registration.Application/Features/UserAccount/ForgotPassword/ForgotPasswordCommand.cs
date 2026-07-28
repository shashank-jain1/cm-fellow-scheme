using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.UserAccount.ForgotPassword;

public sealed record ForgotPasswordCommand : ICommand<Result<ForgotPasswordResponse>>
{
    public string Email { get; init; } = null!;
}

public sealed record ForgotPasswordResponse
{
    public string Message { get; init; } = null!;
    public string? ResetToken { get; init; }
}
