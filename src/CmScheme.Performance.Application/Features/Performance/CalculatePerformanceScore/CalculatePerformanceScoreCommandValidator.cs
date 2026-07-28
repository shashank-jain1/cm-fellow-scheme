using FluentValidation;

namespace CmScheme.Performance.Application.Features.Performance.CalculatePerformanceScore;

public sealed class CalculatePerformanceScoreCommandValidator : AbstractValidator<CalculatePerformanceScoreCommand>
{
    public CalculatePerformanceScoreCommandValidator()
    {
        RuleFor(x => x.PerformanceEvaluationId)
            .GreaterThan(0).WithMessage("Performance Evaluation ID is required.");
    }
}
