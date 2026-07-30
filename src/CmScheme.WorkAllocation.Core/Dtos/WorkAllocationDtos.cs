namespace CmScheme.WorkAllocation.Core.Dtos;

public sealed class WorkAllocationDto
{
    public int WorkAllocationId { get; init; }
    public int ProjectId { get; init; }
    public string WorkProjectId { get; init; } = null!;
    public string WorkDescription { get; init; } = null!;
    public string Priority { get; init; } = null!;
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int DurationDays { get; init; }
    public int SurveysPerIntern { get; init; }
    public int DivisionId { get; init; }
    public int DistrictId { get; init; }
    public int BlockId { get; init; }
    public bool ActiveStatus { get; init; }
    public string Status { get; init; } = null!;
    public int? AssignedToUserId { get; init; }
    public DateTime CreatedOn { get; init; }
    public string CreatedBy { get; init; } = null!;
    public DateTime? ModifiedOn { get; init; }
    public string? ModifiedBy { get; init; }
}

public sealed class TaskProgressDto
{
    public int TaskProgressId { get; init; }
    public int WorkAllocationId { get; init; }
    public string ProjectName { get; init; } = null!;
    public string WorkProject { get; init; } = null!;
    public string WorkDescription { get; init; } = null!;
    public string Priority { get; init; } = null!;
    public int NumberOfSurveys { get; init; }
    public int CompletedSurveys { get; init; }
    public int PendingSurveys { get; init; }
    public DateTime? CompletionDate { get; init; }
    public string WorkStatus { get; init; } = null!;
    public decimal CompletionPercentage { get; init; }
}

public sealed class SurveyRecordDto
{
    public int SurveyRecordId { get; init; }
    public int TaskProgressId { get; init; }
    public string InternName { get; init; } = null!;
    public string SurveyPersonName { get; init; } = null!;
    public string MobileNumber { get; init; } = null!;
    public string PanchayatName { get; init; } = null!;
    public string VillageName { get; init; } = null!;
    public DateTime SurveyDate { get; init; }
    public string SurveyStatus { get; init; } = null!;
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
}

public sealed class TaskVerificationDto
{
    public int TaskVerificationId { get; init; }
    public int WorkAllocationId { get; init; }
    public int VerifiedBy { get; init; }
    public string VerificationStatus { get; init; } = null!;
    public string? Comments { get; init; }
    public DateTime? VerifiedOn { get; init; }
}

public sealed class TaskAttachmentDto
{
    public int TaskAttachmentId { get; init; }
    public int WorkAllocationId { get; init; }
    public int UserAccountId { get; init; }
    public string FileName { get; init; } = null!;
    public string FilePath { get; init; } = null!;
    public long FileSize { get; init; }
    public string ContentType { get; init; } = null!;
    public DateTime CreatedOn { get; init; }
}

public sealed class TaskDeadlineDto
{
    public int TaskDeadlineId { get; init; }
    public int WorkAllocationId { get; init; }
    public DateTime DeadlineDate { get; init; }
    public int ReminderDaysBefore { get; init; }
    public bool IsOverdue { get; init; }
    public DateTime? LastReminderSentOn { get; init; }
    public DateTime CreatedOn { get; init; }
}

public sealed class FellowTaskProgressDto
{
    public int TaskProgressId { get; init; }
    public int WorkAllocationId { get; init; }
    public int? UserAccountId { get; init; }
    public string? ProgressNotes { get; init; }
    public int? ProgressPercentage { get; init; }
    public string? FellowProgressStatus { get; init; }
    public DateTime? CreatedOn { get; init; }
}
