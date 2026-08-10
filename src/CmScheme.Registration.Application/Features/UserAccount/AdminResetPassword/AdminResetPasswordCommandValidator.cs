using FluentValidation;

namespace CmScheme.Registration.Application.Features.UserAccount.AdminResetPassword;

public sealed class AdminResetPasswordCommandValidator : AbstractValidator<AdminResetPasswordCommand>
{
    public AdminResetPasswordCommandValidator()
    {
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("UserAccountId must be greater than 0.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.");
    }
}
