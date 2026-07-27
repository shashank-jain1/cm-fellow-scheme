using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.GetRegistrationById;
using CmScheme.Registration.Core.Dtos;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class Get
{
    public static IEndpointRouteBuilder MapGetEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{applicantId:int}", async (
            int applicantId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            GetRegistrationByIdQuery query = new GetRegistrationByIdQuery { ApplicantId = applicantId };
            ValueTask<Result<ApplicantDto>> result = sender.Send(query, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("GetRegistrationById")
        .Produces<ApplicantDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
