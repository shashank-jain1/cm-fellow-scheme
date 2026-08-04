namespace CmScheme.Performance.Core.Views;

public class TaskProgressView
{
    public int TaskProgressId { get; set; }
    public int WorkAllocationId { get; set; }
    public int CompletedSurveys { get; set; }
    public int TargetSurveys { get; set; }
    public string WorkStatus { get; set; } = null!;
}
