using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.AttendanceLeave.Core.Dtos;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public static class AttendanceGroupExtensions
{
    public static IEndpointRouteBuilder MapAttendanceEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("attendance")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Attendance, "Read", requireScope: false);

        group.MapPost("/", Mark.Handle)
            .WithName("MarkAttendance")
            .WithDisplayName("Mark attendance")
            .DisableAntiforgery()
            .Produces<int>()
            .ProducesValidationProblem();

        group.MapPut("/checkout", CheckOut.Handle)
            .WithName("CheckOutAttendance")
            .WithDisplayName("Check out attendance")
            .DisableAntiforgery()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapGet("/", GetHistory.Handle)
            .WithName("GetAttendanceHistory")
            .WithDisplayName("Get attendance history")
            .Produces<List<AttendanceDto>>();

        group.MapGet("/payroll-summary", GetPayrollSummary.Handle)
            .WithName("GetPayrollSummary")
            .WithDisplayName("Get payroll summary");

        group.MapGet("/report/monthly", GetMonthlyReport.Handle)
            .WithName("GetMonthlyReport")
            .WithDisplayName("Get monthly attendance report");

        group.MapGet("/report/weekly", GetWeeklyReport.Handle)
            .WithName("GetWeeklyReport")
            .WithDisplayName("Get weekly attendance report");

        return group;
    }
}
