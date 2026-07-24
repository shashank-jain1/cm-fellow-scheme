using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.States.CreateState;

public sealed class CreateStateCommandValidator : AbstractValidator<CreateStateCommand>
{
    public CreateStateCommandValidator()
    {
        RuleFor(x => x.StateName)
            .NotEmpty().WithMessage("State name is required.")
            .MaximumLength(100).WithMessage("State name must not exceed 100 characters.");

        RuleFor(x => x.StateCode)
            .NotEmpty().WithMessage("State code is required.")
            .MaximumLength(10).WithMessage("State code must not exceed 10 characters.");

        RuleFor(x => x.StateShortName)
            .MaximumLength(20).WithMessage("State short name must not exceed 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.StateShortName));
    }
}
