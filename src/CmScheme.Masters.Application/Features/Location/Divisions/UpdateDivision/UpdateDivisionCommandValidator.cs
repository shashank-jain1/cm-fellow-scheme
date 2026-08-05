using FluentValidation;

namespace CmScheme.Masters.Application.Features.Location.Divisions.UpdateDivision;

public sealed class UpdateDivisionCommandValidator : AbstractValidator<UpdateDivisionCommand>
{
    public UpdateDivisionCommandValidator()
    {
        RuleFor(x => x.DivisionId)
            .GreaterThan(0).WithMessage("Valid division is required.");

        RuleFor(x => x.StateId)
            .GreaterThan(0).WithMessage("Valid state is required.");

        RuleFor(x => x.DivisionName)
            .NotEmpty().WithMessage("Division name is required.")
            .MaximumLength(100).WithMessage("Division name must not exceed 100 characters.");

        RuleFor(x => x.DivisionCode)
            .MaximumLength(10).WithMessage("Division code must not exceed 10 characters.")
            .When(x => !string.IsNullOrEmpty(x.DivisionCode));
    }
}
