using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class AttendanceGroupExtensions
{
    public static IEndpointRouteBuilder MapAttendanceEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("attendance");

        group.MapPost("/", Mark.Handle);
        group.MapGet("/history", GetHistory.Handle);

        return builder;
    }
}
