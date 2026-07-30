using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.SelfAssessment.SubmitSelfAssessment;

public sealed record SubmitSelfAssessmentCommand : ICommand<Result>
{
    public int UserAccountId { get; init; }
    public int? ReviewCycleId { get; init; }
    public string? Strengths { get; init; }
    public string? Improvements { get; init; }
    public string? GoalsAchieved { get; init; }
    public string? GoalsMissed { get; init; }
    public string? TrainingFeedback { get; init; }
    public int OverallRating { get; init; }
}
