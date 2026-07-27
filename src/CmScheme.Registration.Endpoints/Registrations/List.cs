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
            string? searchTerm,
            string? status,
            int? pageNumber,
            int? pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            ListRegistrationsQuery query = new ListRegistrationsQuery
            {
                SearchTerm = searchTerm ?? string.Empty,
                Status = status ?? string.Empty,
                PageNumber = pageNumber is null or 0 ? 1 : pageNumber.Value,
                PageSize = pageSize is null or 0 ? 10 : pageSize.Value
            };
            ValueTask<Result<List<RegistrationListItem>>> result = sender.Send(query, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("ListRegistrations")
        .Produces<List<RegistrationListItem>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
