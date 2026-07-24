using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Attendance.GetAttendanceHistory;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class GetHistory
{
    public static async Task<IResult> Handle([AsParameters] GetAttendanceHistoryQuery query, ISender sender)
    {
        var result = await sender.Send(query);
        return result.ToApiResult();
    }
}
