using Ardalis.Result;
using CmScheme.AttendanceLeave.Core.Data;
using CmScheme.AttendanceLeave.Core.Dtos;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.AttendanceLeave.Application.Features.Attendance.GetWeeklyReport;

public sealed class GetWeeklyAttendanceReportQueryHandler(IAttendanceLeaveQueryDbContext dbContext)
    : IQueryHandler<GetWeeklyAttendanceReportQuery, Result<WeeklyAttendanceReportDto>>
{
    public async ValueTask<Result<WeeklyAttendanceReportDto>> Handle(
        GetWeeklyAttendanceReportQuery request,
        CancellationToken cancellationToken)
    {
        DateTime weekStartDate = new DateTime(
            request.WeekStartDate.Year,
            request.WeekStartDate.Month,
            request.WeekStartDate.Day,
            0, 0, 0,
            DateTimeKind.Utc);
        DateTime weekEndDate = weekStartDate.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);

        List<Core.Entities.Attendance> attendances = await dbContext.Attendances
            .Where(a =>
                a.ApplicantId == request.ApplicantId &&
                a.AttendanceDate >= weekStartDate &&
                a.AttendanceDate <= weekEndDate)
            .OrderBy(a => a.AttendanceDate)
            .ToListAsync(cancellationToken);

        List<DailyAttendanceRecordDto> dailyRecords = [];
        decimal totalHours = 0;

        foreach (Core.Entities.Attendance record in attendances)
        {
            decimal hoursWorked = 0;
            if (record.CheckOutTime.HasValue)
            {
                TimeOnly checkIn = record.CheckInTime;
                TimeOnly checkOut = record.CheckOutTime.Value;
                hoursWorked = (decimal)(checkOut.ToTimeSpan() - checkIn.ToTimeSpan()).TotalHours;
                hoursWorked = Math.Round(hoursWorked, 2);
                totalHours += hoursWorked;
            }

            DailyAttendanceRecordDto dailyRecord = new DailyAttendanceRecordDto
            {
                AttendanceDate = record.AttendanceDate,
                CheckInTime = record.CheckInTime,
                CheckOutTime = record.CheckOutTime,
                Latitude = record.Latitude,
                Longitude = record.Longitude,
                AttendanceStatus = record.AttendanceStatus,
                HoursWorked = hoursWorked,
            };
            dailyRecords.Add(dailyRecord);
        }

        WeeklyAttendanceReportDto result = new WeeklyAttendanceReportDto
        {
            ApplicantId = request.ApplicantId,
            WeekStartDate = weekStartDate,
            WeekEndDate = weekEndDate,
            TotalAttendanceDays = attendances.Count,
            TotalHours = Math.Round(totalHours, 2),
            DailyRecords = dailyRecords,
        };

        return Result<WeeklyAttendanceReportDto>.Success(result);
    }
}
