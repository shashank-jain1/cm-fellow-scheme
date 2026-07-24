using FluentValidation;

namespace CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;

public sealed class CreateUserAccountCommandValidator : AbstractValidator<CreateUserAccountCommand>
{
    public CreateUserAccountCommandValidator()
    {
        RuleFor(x => x.ApplicantId)
            .GreaterThan(0).WithMessage("Applicant ID must be greater than zero.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.")
            .Matches(@"[!@#$%^&*]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .MaximumLength(50).WithMessage("Role must not exceed 50 characters.");

        RuleFor(x => x.CreatedBy)
            .GreaterThan(0).WithMessage("Created by must be greater than zero.");
    }
}
