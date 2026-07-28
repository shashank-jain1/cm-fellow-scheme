using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Attendance.GetPayrollSummary;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class GetPayrollSummary
{
    public static async Task<IResult> Handle(string? payrollMonth, int? applicantId, ISender sender)
    {
        var result = await sender.Send(new GetPayrollSummaryQuery
        {
            PayrollMonth = payrollMonth,
            ApplicantId = applicantId
        });
        return result.ToApiResult();
    }
}
