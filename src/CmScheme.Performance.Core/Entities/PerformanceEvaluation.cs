namespace CmScheme.Performance.Core.Entities;

public class PerformanceEvaluation
{
    public int PerformanceEvaluationId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string WorkProject { get; set; } = null!;
    public string WorkDescription { get; set; } = null!;
    public string ApplicantNumber { get; set; } = null!;
    public string ApplicantName { get; set; } = null!;
    public string AssignedWork { get; set; } = null!;
    public string WorkingLocation { get; set; } = null!;
    public int TotalSurveysAssigned { get; set; }
    public int SurveysCompleted { get; set; }
    public int SurveysPending { get; set; }
    public int AttendanceDays { get; set; }
    public int LeaveDays { get; set; }
    public int WorkingDays { get; set; }
    public decimal PerformanceScore { get; set; }
    public string PerformanceGrade { get; set; } = null!;
    public decimal? SupervisorRating { get; set; }
    public decimal? QualityScore { get; set; }
    public int RejectedSurveys { get; set; }
    public int ApprovedSurveys { get; set; }
    public decimal CompletionPercentage { get; set; }
    public string PerformanceStatus { get; set; } = null!;
    public string? EvaluationRemarks { get; set; }
    public string? EvaluatedBy { get; set; }
    public DateTime? EvaluationDate { get; set; }
}
