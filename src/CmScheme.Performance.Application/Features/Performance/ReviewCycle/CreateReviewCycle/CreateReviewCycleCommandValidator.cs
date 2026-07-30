using FluentValidation;

namespace CmScheme.Performance.Application.Features.Performance.ReviewCycle.CreateReviewCycle;

public sealed class CreateReviewCycleCommandValidator
    : AbstractValidator<CreateReviewCycleCommand>
{
    public CreateReviewCycleCommandValidator()
    {
        RuleFor(x => x.CycleName)
            .NotEmpty().WithMessage("CycleName is required.")
            .MaximumLength(100).WithMessage("CycleName must not exceed 100 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be after StartDate.");
    }
}
