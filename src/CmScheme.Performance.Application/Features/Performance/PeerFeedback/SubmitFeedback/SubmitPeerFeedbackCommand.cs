using Ardalis.Result;
using Mediator;

namespace CmScheme.Performance.Application.Features.Performance.PeerFeedback.SubmitFeedback;

public sealed record SubmitPeerFeedbackCommand : ICommand<Result<int>>
{
    public int PerformanceEvaluationId { get; init; }
    public int ReviewerId { get; init; }
    public string ReviewerName { get; init; } = null!;
    public int RevieweeId { get; init; }
    public string RevieweeName { get; init; } = null!;
    public decimal TechnicalSkillsRating { get; init; }
    public decimal CommunicationRating { get; init; }
    public decimal TeamworkRating { get; init; }
    public decimal ProblemSolvingRating { get; init; }
    public decimal LeadershipRating { get; init; }
    public string? Strengths { get; init; }
    public string? AreasForImprovement { get; init; }
    public string? AdditionalComments { get; init; }
    public bool IsAnonymized { get; init; }
}
