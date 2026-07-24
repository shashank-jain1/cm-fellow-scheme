using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public sealed class AttendanceEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapAttendanceEndpoints();
    }
}
