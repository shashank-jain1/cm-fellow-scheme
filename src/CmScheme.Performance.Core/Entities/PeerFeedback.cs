namespace CmScheme.Performance.Core.Entities;

public class PeerFeedback
{
    public int PeerFeedbackId { get; set; }
    public int PerformanceEvaluationId { get; set; }
    public int ReviewerId { get; set; }
    public string ReviewerName { get; set; } = null!;
    public int RevieweeId { get; set; }
    public string RevieweeName { get; set; } = null!;
    public decimal TechnicalSkillsRating { get; set; }
    public decimal CommunicationRating { get; set; }
    public decimal TeamworkRating { get; set; }
    public decimal ProblemSolvingRating { get; set; }
    public decimal LeadershipRating { get; set; }
    public decimal OverallRating { get; set; }
    public string? Strengths { get; set; }
    public string? AreasForImprovement { get; set; }
    public string? AdditionalComments { get; set; }
    public bool IsAnonymized { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
