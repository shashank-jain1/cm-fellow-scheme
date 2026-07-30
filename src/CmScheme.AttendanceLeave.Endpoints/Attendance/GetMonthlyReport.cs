using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Attendance.GetMonthlyReport;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class GetMonthlyReport
{
    public static async Task<IResult> Handle(int month, int year, ISender sender, HttpContext httpContext)
    {
        int? applicantId = httpContext.User.FindFirst("UserAccountId") is { } claim
            && int.TryParse(claim.Value, out int parsed)
            ? parsed
            : null;

        if (!applicantId.HasValue)
        {
            return Results.Unauthorized();
        }

        GetMonthlyAttendanceReportQuery query = new GetMonthlyAttendanceReportQuery
        {
            ApplicantId = applicantId.Value,
            Month = month,
            Year = year,
        };

        Result<MonthlyAttendanceReportResult> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
