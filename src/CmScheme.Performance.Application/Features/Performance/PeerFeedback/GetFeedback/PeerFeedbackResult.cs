namespace CmScheme.Performance.Application.Features.Performance.PeerFeedback.GetFeedback;

public sealed record PeerFeedbackResult
{
    public int PeerFeedbackId { get; init; }
    public int PerformanceEvaluationId { get; init; }
    public string ReviewerName { get; init; } = null!;
    public string RevieweeName { get; init; } = null!;
    public decimal TechnicalSkillsRating { get; init; }
    public decimal CommunicationRating { get; init; }
    public decimal TeamworkRating { get; init; }
    public decimal ProblemSolvingRating { get; init; }
    public decimal LeadershipRating { get; init; }
    public decimal OverallRating { get; init; }
    public string? Strengths { get; init; }
    public string? AreasForImprovement { get; init; }
    public string? AdditionalComments { get; init; }
    public bool IsAnonymized { get; init; }
    public string Status { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}
