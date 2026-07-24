namespace CmScheme.WorkAllocation.Core.Entities;

public class TaskProgress
{
    public int TaskProgressId { get; set; }
    public int WorkAllocationId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string WorkProject { get; set; } = null!;
    public string WorkDescription { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public int NumberOfSurveys { get; set; }
    public int CompletedSurveys { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string WorkStatus { get; set; } = null!;
    public decimal CompletionPercentage { get; set; }
}
