using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetPayrollSummary;

public sealed record GetPayrollSummaryQuery : IQuery<Result<List<PayrollSummaryDto>>>
{
    public string? PayrollMonth { get; init; }
    public int? ApplicantId { get; init; }
}

public sealed record PayrollSummaryDto
{
    public int PayrollAttendanceSummaryId { get; init; }
    public int ApplicantId { get; init; }
    public string PayrollMonth { get; init; } = null!;
    public int TotalWorkingDays { get; init; }
    public decimal PresentDays { get; init; }
    public decimal ApprovedLeaveDays { get; init; }
    public decimal AbsentDays { get; init; }
    public decimal PayableDays { get; init; }
    public DateTime CreatedOn { get; init; }
}
