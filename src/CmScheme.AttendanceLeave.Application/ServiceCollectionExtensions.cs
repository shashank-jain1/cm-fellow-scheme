using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using CmScheme.AttendanceLeave.Application.Features.Leave.ApplyLeave;

namespace CmScheme.AttendanceLeave.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAttendanceLeaveApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.Scan(s => s.FromAssemblyOf<ApplyLeaveCommandHandler>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
