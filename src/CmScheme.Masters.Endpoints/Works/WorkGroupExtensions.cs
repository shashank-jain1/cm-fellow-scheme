using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Masters.Endpoints.Works;

public static class WorkGroupExtensions
{
    public static IEndpointRouteBuilder MapWorkEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("masters/works")
            .WithDisplayName("Work Masters")
            .WithTags("Masters")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Masters, "Read", requireScope: false);

        group.MapGet("/", ListWorks.List);
        group.MapGet("/{workId:int}", GetWork.GetById);
        group.MapPost("/", CreateWork.Create);
        group.MapPut("/{workId:int}", UpdateWork.Update);

        return builder;
    }
}
