using Microsoft.AspNetCore.Routing;

namespace CmScheme.AttendanceLeave.Endpoints.Attendance;

public interface IAttendanceEndpoints
{
    void Configure(IEndpointRouteBuilder builder);
}
