using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.Leave.GetLeaveStatus;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Leave;

public static class GetStatus
{
    public static async Task<IResult> Handle([AsParameters] GetLeaveStatusQuery query, ISender sender)
    {
        var result = await sender.Send(query);
        return result.ToApiResult();
    }
}
