using Microsoft.Extensions.DependencyInjection;
using CmScheme.Endpoints.Abstractions;
using CmScheme.AttendanceLeave.Application;
using CmScheme.AttendanceLeave.Endpoints.Attendance;

namespace CmScheme.AttendanceLeave.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAttendanceLeaveApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<AttendanceEndpoints>();
        return services;
    }

    public static IServiceCollection AddAttendanceLeaveServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddAttendanceLeaveApplication();
        return services;
    }
}
