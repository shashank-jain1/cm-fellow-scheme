using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;

namespace CmScheme.Masters.Endpoints.Projects;

public sealed class ProjectEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder group = builder.MapProjectGroup();

        group.MapGet("/", ListProjects.List)
            .WithTags("Projects")
            .WithName("ListProjects")
            .WithDisplayName("List all projects");

        group.MapPost("/", CreateProject.Create)
            .WithTags("Projects")
            .WithName("CreateProject")
            .WithDisplayName("Create a new project");
    }
}
