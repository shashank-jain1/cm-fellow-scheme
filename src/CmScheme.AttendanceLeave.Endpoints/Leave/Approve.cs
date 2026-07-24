using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Leave.ApproveLeave;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Leave;

public static class Approve
{
    public static async Task<IResult> Handle(ApproveLeaveCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
