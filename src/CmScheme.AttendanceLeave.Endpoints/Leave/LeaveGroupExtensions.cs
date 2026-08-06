using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.AttendanceLeave.Endpoints.Leave;

public static class LeaveGroupExtensions
{
    public static IEndpointRouteBuilder MapLeaveEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("leave")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Attendance, "Read", requireScope: false);

        group.MapPost("/", Apply.Handle).DisableAntiforgery();
        group.MapGet("/status", GetStatus.Handle);
        group.MapGet("/balance", GetBalance.Handle);
        group.MapPut("/approve", Approve.Handle).DisableAntiforgery();

        return group;
    }
}
