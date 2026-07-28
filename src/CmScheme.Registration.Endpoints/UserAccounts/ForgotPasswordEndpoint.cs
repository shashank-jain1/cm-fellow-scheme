using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.ForgotPassword;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class ForgotPasswordEndpoint
{
    public static void MapForgotPasswordEndpoint(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/user-accounts");

        group.MapPost("/forgot-password", async (ForgotPasswordCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
        })
        .WithName("ForgotPassword")
        .WithDisplayName("Request password reset");
    }
}
