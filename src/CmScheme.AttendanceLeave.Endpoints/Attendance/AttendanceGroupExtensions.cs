using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class AttendanceGroupExtensions
{
    public static IEndpointRouteBuilder MapAttendanceEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("attendance");

        group.MapPost("/", Mark.Handle);
        group.MapPut("/checkout", CheckOut.Handle);
        group.MapGet("/history", GetHistory.Handle);
        group.MapGet("/payroll-summary", GetPayrollSummary.Handle);

        return builder;
    }
}
