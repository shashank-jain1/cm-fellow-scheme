using Microsoft.AspNetCore.Http;
using CmScheme.Registration.Application.Features.Leave.ApplyLeave;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Leave;

public static class Apply
{
    public static async Task<IResult> Handle(ApplyLeaveCommand command, ISender sender)
    {
        var result = await sender.Send(command);
        return result.ToApiResult();
    }
}
