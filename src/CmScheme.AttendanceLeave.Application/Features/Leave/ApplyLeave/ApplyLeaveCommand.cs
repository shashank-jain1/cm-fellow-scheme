using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

public sealed record ApplyLeaveCommand : ICommand<Result<int>>
{
    public int ApplicantId { get; init; }
    public string LeaveType { get; init; } = string.Empty;
    public DateTime FromDate { get; init; }
    public DateTime ToDate { get; init; }
    public int NumberOfDays { get; init; }
    public string HalfDayFullDay { get; init; } = string.Empty;
    public string LeaveReason { get; init; } = string.Empty;
    public string? AttachmentPath { get; init; }
    public string ReportingManagerName { get; init; } = string.Empty;
    public string CreatedBy { get; init; } = string.Empty;
}
