using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Endpoints.Abstractions.Authorization;
using CmScheme.Endpoints.Abstractions.Extensions;
using CmScheme.Masters.Application.Features.Department.CreateDepartment;
using CmScheme.Masters.Core.Data;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.Masters.Endpoints.Departments;

public sealed class DepartmentEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/masters/departments")
            .WithTags("Departments")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Masters, "Read", requireScope: false);

        group.MapGet("", List.Handle)
            .WithName("ListDepartments")
            .WithDisplayName("List departments");

        group.MapPost("", Create.Handle)
            .WithName("CreateDepartment")
            .WithDisplayName("Create a department")
            .DisableAntiforgery();
    }
}

internal static class List
{
    public static async Task<IResult> Handle(IMastersQueryDbContext dbContext)
    {
        var departments = await dbContext.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.DepartmentName)
            .Select(d => new { d.DepartmentId, d.DepartmentName, d.DepartmentCode, d.IsActive })
            .ToListAsync();

        return Results.Ok(departments);
    }
}

internal static class Create
{
    public static async Task<IResult> Handle(
        CreateDepartmentCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<int> result = await sender.Send(command, cancellationToken);
        return result.ToApiResult();
    }
}
