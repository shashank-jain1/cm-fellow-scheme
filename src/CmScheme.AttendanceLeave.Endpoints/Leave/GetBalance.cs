using Microsoft.AspNetCore.Http;
using CmScheme.AttendanceLeave.Application.Features.Leave.GetLeaveBalance;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.AttendanceLeave.Endpoints.Leave;

public static class GetBalance
{
    public static async Task<IResult> Handle([AsParameters] GetLeaveBalanceQuery query, ISender sender)
    {
        var result = await sender.Send(query);
        return result.ToApiResult();
    }
}
