using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Registration.Endpoints.Auth;

public static class AuthGroupExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/auth")
            .WithTags("Authentication");

        group.MapPost("login", Login.Handle)
            .AllowAnonymous()
            .WithName("Login")
            .WithDisplayName("Login with username and password");

        group.MapPost("register", Register.Handle)
            .AllowAnonymous()
            .WithName("Register")
            .WithDisplayName("Register a new user account");

        return group;
    }
}
