using FluentValidation;

namespace CmScheme.Registration.Application.Features.ExitInterview.SubmitExitInterview;

public sealed class SubmitExitInterviewCommandValidator
    : AbstractValidator<SubmitExitInterviewCommand>
{
    public SubmitExitInterviewCommandValidator()
    {
        RuleFor(x => x.UserAccountId)
            .GreaterThan(0).WithMessage("UserAccountId is required.");

        RuleFor(x => x.OverallExperience)
            .InclusiveBetween(1, 5).WithMessage("OverallExperience must be between 1 and 5.");

        RuleFor(x => x.WorkEnvironment)
            .InclusiveBetween(1, 5).WithMessage("WorkEnvironment must be between 1 and 5.");

        RuleFor(x => x.LearningOpportunities)
            .InclusiveBetween(1, 5).WithMessage("LearningOpportunities must be between 1 and 5.");

        RuleFor(x => x.TeamCollaboration)
            .InclusiveBetween(1, 5).WithMessage("TeamCollaboration must be between 1 and 5.");

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(1000).WithMessage("ImprovementSuggestions must not exceed 1000 characters.");

        RuleFor(x => x.WhatWorkedWell)
            .MaximumLength(1000).WithMessage("WhatWorkedWell must not exceed 1000 characters.");
    }
}
