using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.AssignRole;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class AssignRole
{
    public static IEndpointRouteBuilder MapAssignRoleEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/{userAccountId:int}/assign-role", async (
            int userAccountId,
            AssignRoleRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            AssignRoleCommand command = new AssignRoleCommand { UserAccountId = userAccountId, Role = request.Role, ModifiedBy = request.ModifiedBy };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("AssignRole")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record AssignRoleRequest(string Role, int ModifiedBy);
