using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.AttendanceLeave.Endpoints.Holidays;

public static class HolidayGroupExtensions
{
    public static IEndpointRouteBuilder MapHolidayEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("holidays")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Attendance, "Read", requireScope: false);

        group.MapGet("/", ListHolidays.Handle);
        group.MapPost("/", CreateHoliday.Handle).DisableAntiforgery();
        group.MapPut("/{holidayId:int}", UpdateHoliday.Handle).DisableAntiforgery();
        group.MapDelete("/{holidayId:int}", DeleteHoliday.Handle);

        return group;
    }
}
