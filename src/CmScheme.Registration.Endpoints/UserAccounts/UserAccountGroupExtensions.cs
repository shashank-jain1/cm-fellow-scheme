using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Registration.Endpoints.UserAccounts;

public static class UserAccountGroupExtensions
{
    public static IEndpointRouteBuilder MapUserAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/user-accounts")
            .WithTags("User Accounts");

        group.MapCreateEndpoint();
        group.MapAssignRoleEndpoint();
        group.MapDeactivateEndpoint();

        return builder;
    }
}
