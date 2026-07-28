using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.AttendanceLeave.Endpoints.Holidays;

public static class HolidayGroupExtensions
{
    public static IEndpointRouteBuilder MapHolidayEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("holidays");

        group.MapGet("/", ListHolidays.Handle);
        group.MapPost("/", CreateHoliday.Handle);
        group.MapPut("/{holidayId:int}", UpdateHoliday.Handle);
        group.MapDelete("/{holidayId:int}", DeleteHoliday.Handle);

        return builder;
    }
}
