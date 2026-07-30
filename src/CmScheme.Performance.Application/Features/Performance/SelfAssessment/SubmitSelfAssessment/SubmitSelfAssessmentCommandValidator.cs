using FluentValidation;

namespace CmScheme.Performance.Application.Features.Performance.SelfAssessment.SubmitSelfAssessment;

public sealed class SubmitSelfAssessmentCommandValidator
    : AbstractValidator<SubmitSelfAssessmentCommand>
{
    public SubmitSelfAssessmentCommandValidator()
    {
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("UserAccountId is required.");

        RuleFor(x => x.OverallRating)
            .InclusiveBetween(1, 10).WithMessage("OverallRating must be between 1 and 10.");

        RuleFor(x => x.Strengths)
            .MaximumLength(1000).WithMessage("Strengths must not exceed 1000 characters.");

        RuleFor(x => x.Improvements)
            .MaximumLength(1000).WithMessage("Improvements must not exceed 1000 characters.");

        RuleFor(x => x.GoalsAchieved)
            .MaximumLength(1000).WithMessage("GoalsAchieved must not exceed 1000 characters.");

        RuleFor(x => x.GoalsMissed)
            .MaximumLength(1000).WithMessage("GoalsMissed must not exceed 1000 characters.");

        RuleFor(x => x.TrainingFeedback)
            .MaximumLength(1000).WithMessage("TrainingFeedback must not exceed 1000 characters.");
    }
}
