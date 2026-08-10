using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.ForgotPassword;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class ForgotPasswordEndpoint
{
    /// <summary>
    /// Mapped on the root builder rather than the authenticated /user-accounts group:
    /// a user who has lost their password cannot present a token.
    /// </summary>
    public static void MapForgotPasswordEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/user-accounts/forgot-password", async (ForgotPasswordCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
        })
        .AllowAnonymous()
        .WithTags("User Accounts")
        .WithName("ForgotPassword")
        .WithDisplayName("Request password reset");
    }
}
