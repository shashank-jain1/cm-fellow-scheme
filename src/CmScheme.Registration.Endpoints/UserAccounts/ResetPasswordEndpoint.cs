using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.ResetPassword;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class ResetPasswordEndpoint
{
    public static void MapResetPasswordEndpoint(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/user-accounts");

        group.MapPost("/reset-password", async (ResetPasswordCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
        })
        .WithName("ResetPassword")
        .WithDisplayName("Reset password with token");
    }
}
