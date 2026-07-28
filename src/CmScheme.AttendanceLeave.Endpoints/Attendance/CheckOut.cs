using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Attendance.CheckOutAttendance;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class CheckOut
{
    public static async Task<IResult> Handle(CheckOutAttendanceCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
