using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.Common.Core;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetMonthlyReport;

public sealed class GetMonthlyAttendanceReportQueryHandler(IAttendanceLeaveQueryDbContext dbContext)
    : IQueryHandler<GetMonthlyAttendanceReportQuery, Result<MonthlyAttendanceReportResult>>
{
    public async ValueTask<Result<MonthlyAttendanceReportResult>> Handle(
        GetMonthlyAttendanceReportQuery request,
        CancellationToken cancellationToken)
    {
        DateTime startDate = new DateTime(request.Year, request.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime endDate = startDate.AddMonths(1).AddDays(-1);

        List<Core.Entities.Attendance> attendances = await dbContext.Attendances
            .Where(a =>
                a.ApplicantId == request.ApplicantId &&
                a.AttendanceDate >= startDate &&
                a.AttendanceDate <= endDate)
            .ToListAsync(cancellationToken);

        int attendanceDays = attendances.Count;
        decimal totalHours = 0;

        foreach (Core.Entities.Attendance record in attendances)
        {
            if (record.CheckOutTime.HasValue)
            {
                TimeOnly checkIn = record.CheckInTime;
                TimeOnly checkOut = record.CheckOutTime.Value;
                totalHours += (decimal)(checkOut.ToTimeSpan() - checkIn.ToTimeSpan()).TotalHours;
            }
        }

        int daysInMonth = DateTime.DaysInMonth(request.Year, request.Month);
        int absentDays = daysInMonth - attendanceDays;

        int leaveDays = await dbContext.LeaveApplications
            .CountAsync(la =>
                la.ApplicantId == request.ApplicantId &&
                la.Status == Statuses.Leave.Approved &&
                la.FromDate <= endDate &&
                la.ToDate >= startDate,
                cancellationToken);

        MonthlyAttendanceReportResult result = new MonthlyAttendanceReportResult
        {
            AttendanceDays = attendanceDays,
            TotalHours = Math.Round(totalHours, 2),
            AbsentDays = Math.Max(0, absentDays - leaveDays),
            LeaveDays = leaveDays,
        };

        return Result<MonthlyAttendanceReportResult>.Success(result);
    }
}
