using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Certificate.Endpoints.Exit;

public static class ExitGroupExtensions
{
    public static IEndpointRouteBuilder MapExitEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("exit");

        group.MapPost("/readiness", SubmitReadiness.Handle);
        group.MapPut("/compliance", VerifyCompliance.Handle);

        return builder;
    }
}
