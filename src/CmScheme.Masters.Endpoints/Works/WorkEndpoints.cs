using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;

namespace CmScheme.Masters.Endpoints.Works;

public sealed class WorkEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder group = builder.MapWorkGroup();

        group.MapGet("/", ListWorks.List)
            .WithTags("Works")
            .WithName("ListWorks")
            .WithDisplayName("List works by project");

        group.MapPost("/", CreateWork.Create)
            .WithTags("Works")
            .WithName("CreateWork")
            .WithDisplayName("Create a new work");
    }
}
