using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Attendance.MarkAttendance;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class Mark
{
    public static async Task<IResult> Handle(MarkAttendanceCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
