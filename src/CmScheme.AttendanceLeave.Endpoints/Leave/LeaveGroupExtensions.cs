using Microsoft.AspNetCore.Builder;
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

        return group;
    }
}
