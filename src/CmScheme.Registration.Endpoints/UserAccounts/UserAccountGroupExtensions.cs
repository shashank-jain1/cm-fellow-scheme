using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class UserAccountGroupExtensions
{
    public static IEndpointRouteBuilder MapUserAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/user-accounts")
            .WithTags("User Accounts")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Registration, "Write", requireScope: false);

        group.MapGet("/", List.Handle)
            .WithName("ListUserAccounts")
            .WithDisplayName("List all user accounts");

        group.MapCreateEndpoint();
        group.MapAssignRoleEndpoint();
        group.MapDeactivateEndpoint();
        group.MapForgotPasswordEndpoint();
        group.MapResetPasswordEndpoint();

        return group;
    }
}
