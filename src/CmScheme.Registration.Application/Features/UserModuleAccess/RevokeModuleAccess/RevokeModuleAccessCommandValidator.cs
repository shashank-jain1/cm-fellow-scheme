using FluentValidation;

namespace CmScheme.Registration.Application.Features.UserModuleAccess.RevokeModuleAccess;

public sealed class RevokeModuleAccessCommandValidator : AbstractValidator<RevokeModuleAccessCommand>
{
    public RevokeModuleAccessCommandValidator()
    {
        RuleFor(x => x.UserModuleAccessId)
            .GreaterThan(0).WithMessage("UserModuleAccessId must be greater than 0.");
        RuleFor(x => x.PerformedBy)
            .GreaterThan(0).WithMessage("PerformedBy must be greater than 0.");
    }
}
