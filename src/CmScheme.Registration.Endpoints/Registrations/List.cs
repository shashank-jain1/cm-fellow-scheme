using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.ListRegistrations;
using CmScheme.Registration.Core.Dtos;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class List
{
    public static IEndpointRouteBuilder MapListEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", async (
            [AsParameters] string? searchTerm,
            [AsParameters] string? status,
            [AsParameters] int? pageNumber,
            [AsParameters] int? pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            ListRegistrationsQuery query = new ListRegistrationsQuery(
                searchTerm,
                status,
                pageNumber ?? 1,
                pageSize ?? 10);
            ValueTask<Result<List<RegistrationListItem>>> result = sender.Send(query, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("ListRegistrations")
        .Produces<List<RegistrationListItem>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
