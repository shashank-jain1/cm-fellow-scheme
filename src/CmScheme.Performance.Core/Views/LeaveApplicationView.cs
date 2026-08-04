namespace CmScheme.Performance.Core.Views;

public class LeaveApplicationView
{
    public int LeaveApplicationId { get; set; }
    public int ApplicantId { get; set; }
    public string LeaveType { get; set; } = null!;
    public decimal NumberOfDays { get; set; }
    public string Status { get; set; } = null!;
}
