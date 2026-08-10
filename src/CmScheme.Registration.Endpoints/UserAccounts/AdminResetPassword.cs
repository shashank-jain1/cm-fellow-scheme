using Ardalis.Result;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Registration.Application.Features.UserAccount.AdminResetPassword;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class AdminResetPassword
{
    public sealed record AdminResetPasswordRequest(string NewPassword);

    public static IEndpointRouteBuilder MapAdminResetPasswordEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/{userAccountId:int}/reset-password", async (
            int userAccountId,
            AdminResetPasswordRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            AdminResetPasswordCommand command = new AdminResetPasswordCommand
            {
                UserAccountId = userAccountId,
                NewPassword = request.NewPassword,
            };
            ValueTask<Result> result = sender.Send(command, cancellationToken);
            return await result.ToApiResultAsync();
        })
        .WithName("AdminResetUserPassword")
        .WithDisplayName("Reset a user's password as an administrator")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return builder;
    }
}
