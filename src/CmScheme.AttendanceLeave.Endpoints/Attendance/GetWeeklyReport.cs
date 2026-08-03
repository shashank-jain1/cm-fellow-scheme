using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Attendance.GetWeeklyReport;
using CmScheme.AttendanceLeave.Core.Dtos;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class GetWeeklyReport
{
    public static async Task<IResult> Handle(DateTime weekStartDate, ISender sender, HttpContext httpContext)
    {
        int? applicantId = httpContext.User.FindFirst("UserAccountId") is { } claim
            && int.TryParse(claim.Value, out int parsed)
            ? parsed
            : null;

        if (!applicantId.HasValue)
        {
            return Results.Unauthorized();
        }

        GetWeeklyAttendanceReportQuery query = new GetWeeklyAttendanceReportQuery
        {
            ApplicantId = applicantId.Value,
            WeekStartDate = weekStartDate,
        };

        Result<WeeklyAttendanceReportDto> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
