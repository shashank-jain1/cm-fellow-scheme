using CmScheme.Endpoints.Abstractions;
using CmScheme.WorkAllocation.Endpoints.WorkAllocations;
using CmScheme.WorkAllocation.Endpoints.TaskProgresses;
using CmScheme.WorkAllocation.Endpoints.SurveyRecords;
using CmScheme.WorkAllocation.Application;
using Microsoft.Extensions.DependencyInjection;

namespace CmScheme.WorkAllocation.Endpoints;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkAllocationApis(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddApiEndpointsFromAssemblyOf<WorkAllocationEndpoints>();
        return services;
    }

    public static IServiceCollection AddWorkAllocationServices(
        this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddWorkAllocationApplication();
        return services;
    }
}
