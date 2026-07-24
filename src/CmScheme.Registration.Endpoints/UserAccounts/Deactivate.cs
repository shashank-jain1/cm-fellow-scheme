using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.DeactivateUserAccount;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class Deactivate
{
    public static IEndpointRouteBuilder MapDeactivateEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/{userAccountId:int}/deactivate", async (
            int userAccountId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            DeactivateUserAccountCommand command = new DeactivateUserAccountCommand(userAccountId);
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("DeactivateUserAccount")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
