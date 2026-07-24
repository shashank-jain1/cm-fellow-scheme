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
