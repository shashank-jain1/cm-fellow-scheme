using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.AttendanceLeave.Endpoints.Leave;

public static class LeaveGroupExtensions
{
    public static IEndpointRouteBuilder MapLeaveEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("leave");

        group.MapPost("/", Apply.Handle);
        group.MapPut("/approve", Approve.Handle);
        group.MapGet("/status", GetStatus.Handle);
        group.MapGet("/balance", GetBalance.Handle);

        return builder;
    }
}
