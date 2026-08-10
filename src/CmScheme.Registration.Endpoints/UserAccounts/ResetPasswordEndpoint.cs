using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.ResetPassword;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class ResetPasswordEndpoint
{
    /// <summary>
    /// Mapped on the root builder rather than the authenticated /user-accounts group:
    /// the reset token issued by forgot-password is the only credential the caller has.
    /// </summary>
    public static void MapResetPasswordEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/user-accounts/reset-password", async (ResetPasswordCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Errors);
        })
        .AllowAnonymous()
        .WithTags("User Accounts")
        .WithName("ResetPassword")
        .WithDisplayName("Reset password with token");
    }
}
