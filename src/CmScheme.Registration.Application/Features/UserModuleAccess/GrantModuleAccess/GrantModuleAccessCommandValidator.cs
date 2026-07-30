using FluentValidation;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.GrantModuleAccess;

public sealed class GrantModuleAccessCommandValidator : AbstractValidator<GrantModuleAccessCommand>
{
    public GrantModuleAccessCommandValidator()
    {
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("User account ID must be greater than zero.");

        RuleFor(x => x.ModuleMasterId)
            .GreaterThan(0).WithMessage("Module master ID must be greater than zero.");

        RuleFor(x => x.PerformedBy)
            .GreaterThan(0).WithMessage("Performed by must be greater than zero.");
    }
}
