using Ardalis.Result;
using Mediator;

namespace CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

public sealed record ApplyLeaveCommand(
    int ApplicantId,
    string LeaveType,
    DateTime FromDate,
    DateTime ToDate,
    int NumberOfDays,
    string HalfDayFullDay,
    string LeaveReason,
    string? AttachmentPath,
    string ReportingManagerName,
    string CreatedBy
) : ICommand<Result<int>>;
