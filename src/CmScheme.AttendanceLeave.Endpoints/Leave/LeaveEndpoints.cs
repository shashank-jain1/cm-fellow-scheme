using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.AttendanceLeave.Endpoints.Leave;

public sealed class LeaveEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapLeaveEndpoints();
    }
}
