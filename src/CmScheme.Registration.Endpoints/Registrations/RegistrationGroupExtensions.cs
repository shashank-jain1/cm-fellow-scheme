using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class RegistrationGroupExtensions
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/registrations")
            .WithTags("Registrations")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Registration, "Read", requireScope: false);

        return group;
    }
}
