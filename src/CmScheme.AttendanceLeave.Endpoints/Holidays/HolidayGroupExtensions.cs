using Microsoft.AspNetCore.Builder;
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

        return group;
    }
}
