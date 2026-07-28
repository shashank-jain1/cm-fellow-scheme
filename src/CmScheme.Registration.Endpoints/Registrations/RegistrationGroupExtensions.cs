using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Registration.Endpoints.Registrations;

public static class RegistrationGroupExtensions
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/registrations")
            .WithTags("Registrations");

        group.MapSubmitEndpoint();
        group.MapGetEndpoint();
        group.MapListEndpoint();
        group.MapApproveEndpoint();
        group.MapRejectEndpoint();
        group.MapVerifyOtpEndpoint();

        return builder;
    }
}
