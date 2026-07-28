using FluentValidation;

namespace CmScheme.Performance.Application.Features.Performance.SubmitPerformanceReview;

public sealed class SubmitPerformanceReviewCommandValidator : AbstractValidator<SubmitPerformanceReviewCommand>
{
    public SubmitPerformanceReviewCommandValidator()
    {
        RuleFor(x => x.PerformanceEvaluationId)
            .GreaterThan(0).WithMessage("PerformanceEvaluationId is required.");

        RuleFor(x => x.Action)
            .NotEmpty().WithMessage("Action is required.")
            .Must(a => new[] { "Submit", "Approve", "Reject" }.Contains(a, StringComparer.OrdinalIgnoreCase))
            .WithMessage("Action must be Submit, Approve, or Reject.");

        RuleFor(x => x.PerformedBy)
            .NotEmpty().WithMessage("PerformedBy is required.")
            .MaximumLength(200).WithMessage("PerformedBy must not exceed 200 characters.");

        RuleFor(x => x.Remarks)
            .MaximumLength(2000).WithMessage("Remarks must not exceed 2000 characters.");
    }
}
