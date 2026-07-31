namespace CmScheme.Performance.Core.Dtos;

public sealed class PerformanceSummaryDto
{
    public int PerformanceEvaluationId { get; init; }
    public string ProjectName { get; init; } = null!;
    public string ApplicantNumber { get; init; } = null!;
    public string ApplicantName { get; init; } = null!;
    public int TotalSurveysAssigned { get; init; }
    public int SurveysCompleted { get; init; }
    public int SurveysPending { get; init; }
    public int AttendanceDays { get; init; }
    public int LeaveDays { get; init; }
    public int WorkingDays { get; init; }
    public decimal PerformanceScore { get; init; }
    public string PerformanceGrade { get; init; } = null!;
    public decimal? SupervisorRating { get; init; }
    public decimal? QualityScore { get; init; }
    public decimal CompletionPercentage { get; init; }
    public string PerformanceStatus { get; init; } = null!;
    public string? EvaluationRemarks { get; init; }
}
