using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.SelfAssessment.GetSelfAssessment;

public sealed record GetSelfAssessmentQuery : IQuery<Result<SelfAssessmentDto>>
{
    public int? UserAccountId { get; init; }
    public int? ReviewCycleId { get; init; }
}

public sealed record SelfAssessmentDto
{
    public int SelfAssessmentId { get; init; }
    public int UserAccountId { get; init; }
    public int? ReviewCycleId { get; init; }
    public string? Strengths { get; init; }
    public string? Improvements { get; init; }
    public string? GoalsAchieved { get; init; }
    public string? GoalsMissed { get; init; }
    public string? TrainingFeedback { get; init; }
    public int OverallRating { get; init; }
    public DateTime SubmittedOn { get; init; }
    public string Status { get; init; } = null!;
}
