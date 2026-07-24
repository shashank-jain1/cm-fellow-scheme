using CmScheme.AttendanceLeave.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.AttendanceLeave.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAttendanceLeaveInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AttendanceLeaveDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IAttendanceLeaveCommandDbContext>(provider =>
            new AttendanceLeaveCommandDbContext(provider.GetRequiredService<AttendanceLeaveDbContext>()));

        services.AddScoped<IAttendanceLeaveQueryDbContext>(provider =>
            new AttendanceLeaveQueryDbContext(provider.GetRequiredService<AttendanceLeaveDbContext>()));

        return services;
    }
}
