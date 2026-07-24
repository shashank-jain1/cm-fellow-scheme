using FluentValidation;

namespace CmScheme.Registration.Application.Features.UserAccount.AssignRole;

public sealed class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
{
    public AssignRoleCommandValidator()
    {
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("User account ID must be greater than zero.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .MaximumLength(50).WithMessage("Role must not exceed 50 characters.");

        RuleFor(x => x.ModifiedBy)
            .GreaterThan(0).WithMessage("Modified by must be greater than zero.");
    }
}
