namespace CmScheme.WorkAllocation.Core.Dtos;

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
