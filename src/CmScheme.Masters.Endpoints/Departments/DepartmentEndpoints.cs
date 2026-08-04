using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.Masters.Core.Data;

namespace CmScheme.Masters.Endpoints.Departments;

public sealed class DepartmentEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/masters/departments")
            .WithTags("Departments")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Masters, "Read", requireScope: false);

        group.MapGet("", List.Handle);
    }
}

internal static class List
{
    public static async Task<IResult> Handle(IMastersQueryDbContext dbContext)
    {
        var departments = await dbContext.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.DepartmentName)
            .Select(d => new { d.DepartmentId, d.DepartmentName, d.DepartmentCode })
            .ToListAsync();

        return Results.Ok(departments);
    }
}
