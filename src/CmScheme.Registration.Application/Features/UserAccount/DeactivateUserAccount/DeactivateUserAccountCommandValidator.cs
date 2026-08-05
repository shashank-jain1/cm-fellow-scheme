using FluentValidation;

namespace CmScheme.Registration.Application.Features.UserAccount.DeactivateUserAccount;

public sealed class DeactivateUserAccountCommandValidator : AbstractValidator<DeactivateUserAccountCommand>
{
    public DeactivateUserAccountCommandValidator()
    {
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("UserAccountId must be greater than 0.");
    }
}
