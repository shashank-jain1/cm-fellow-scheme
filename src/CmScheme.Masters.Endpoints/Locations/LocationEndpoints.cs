using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;

namespace CmScheme.Masters.Endpoints.Locations;

public sealed class LocationEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        IEndpointRouteBuilder group = builder.MapLocationGroup();

        group.MapGet("states", ListStates.List)
            .WithTags("Locations")
            .WithName("ListStates")
            .WithDisplayName("List all states");

        group.MapGet("divisions", ListDivisions.List)
            .WithTags("Locations")
            .WithName("ListDivisions")
            .WithDisplayName("List divisions by state");

        group.MapGet("districts", ListDistricts.List)
            .WithTags("Locations")
            .WithName("ListDistricts")
            .WithDisplayName("List districts by division");

        group.MapGet("blocks", ListBlocks.List)
            .WithTags("Locations")
            .WithName("ListBlocks")
            .WithDisplayName("List blocks by district");

        group.MapGet("gram-panchayats", ListGramPanchayats.List)
            .WithTags("Locations")
            .WithName("ListGramPanchayats")
            .WithDisplayName("List gram panchayats by block");

        group.MapPost("states", CreateState.Create)
            .WithTags("Locations")
            .WithName("CreateState")
            .WithDisplayName("Create a new state");

        group.MapPost("divisions", CreateDivision.Create)
            .WithTags("Locations")
            .WithName("CreateDivision")
            .WithDisplayName("Create a new division");

        group.MapPost("districts", CreateDistrict.Create)
            .WithTags("Locations")
            .WithName("CreateDistrict")
            .WithDisplayName("Create a new district");

        group.MapPost("blocks", CreateBlock.Create)
            .WithTags("Locations")
            .WithName("CreateBlock")
            .WithDisplayName("Create a new block");

        group.MapPost("gram-panchayats", CreateGramPanchayat.Create)
            .WithTags("Locations")
            .WithName("CreateGramPanchayat")
            .WithDisplayName("Create a new gram panchayat");
    }
}
