using FluentValidation;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.BulkUpdateModuleAccess;

public sealed class BulkUpdateModuleAccessCommandValidator : AbstractValidator<BulkUpdateModuleAccessCommand>
{
    public BulkUpdateModuleAccessCommandValidator()
    {
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("User account ID must be greater than zero.");

        RuleFor(x => x.Accesses)
            .NotEmpty().WithMessage("Accesses list must not be empty.");

        RuleFor(x => x.PerformedBy)
            .GreaterThan(0).WithMessage("Performed by must be greater than zero.");

        RuleForEach(x => x.Accesses).ChildRules(item =>
        {
            item.RuleFor(x => x.ModuleMasterId)
                .GreaterThan(0).WithMessage("Module master ID must be greater than zero.");

            item.RuleFor(x => x.ModuleCode)
                .NotEmpty().WithMessage("Module code is required.")
                .MaximumLength(100).WithMessage("Module code must not exceed 100 characters.");

            item.RuleFor(x => x.ModuleName)
                .NotEmpty().WithMessage("Module name is required.")
                .MaximumLength(200).WithMessage("Module name must not exceed 200 characters.");
        });
    }
}
