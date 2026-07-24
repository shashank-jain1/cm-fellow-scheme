using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.CreateUserAccount;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class Create
{
    public static IEndpointRouteBuilder MapCreateEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", async (
            CreateUserAccountRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            CreateUserAccountCommand command = new CreateUserAccountCommand(
                request.ApplicantId,
                request.Username,
                request.Password,
                request.Role,
                request.CreatedBy);
            ValueTask<Result<int>> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("CreateUserAccount")
        .Produces<int>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}

public sealed record CreateUserAccountRequest(
    int ApplicantId,
    string Username,
    string Password,
    string Role,
    int CreatedBy);
