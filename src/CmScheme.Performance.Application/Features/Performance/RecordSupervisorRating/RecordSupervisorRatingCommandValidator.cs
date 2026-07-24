using FluentValidation;

namespace CmScheme.Performance.Application.Features.Performance.RecordSupervisorRating;

public sealed class RecordSupervisorRatingCommandValidator : AbstractValidator<RecordSupervisorRatingCommand>
{
    public RecordSupervisorRatingCommandValidator()
    {
        RuleFor(x => x.PerformanceEvaluationId)
            .GreaterThan(0).WithMessage("PerformanceEvaluationId must be greater than 0.");
        RuleFor(x => x.SupervisorRating)
            .InclusiveBetween(1, 10).WithMessage("SupervisorRating must be between 1 and 10.");
        RuleFor(x => x.EvaluatedBy)
            .NotEmpty().WithMessage("EvaluatedBy is required.")
            .MaximumLength(200).WithMessage("EvaluatedBy must not exceed 200 characters.");
    }
}
