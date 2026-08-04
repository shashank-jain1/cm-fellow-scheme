namespace CmScheme.Performance.Core.Views;

public class WorkAllocationView
{
    public int WorkAllocationId { get; set; }
    public int? AssignedToUserId { get; set; }
    public string Status { get; set; } = null!;
    public int SurveysPerIntern { get; set; }
}
