using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.Registration.SubmitRegistration;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class Submit
{
    public static IEndpointRouteBuilder MapSubmitEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", async (
            SubmitRegistrationCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            ValueTask<Result<int>> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("SubmitRegistration")
        .Produces<int>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
