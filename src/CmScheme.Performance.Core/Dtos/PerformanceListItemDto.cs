namespace CmScheme.Performance.Core.Dtos;

public sealed class PerformanceListItemDto
{
    public int PerformanceEvaluationId { get; init; }
    public string ProjectName { get; init; } = null!;
    public string ApplicantNumber { get; init; } = null!;
    public string ApplicantName { get; init; } = null!;
    public decimal PerformanceScore { get; init; }
    public string PerformanceGrade { get; init; } = null!;
    public decimal CompletionPercentage { get; init; }
    public string PerformanceStatus { get; init; } = null!;
}
