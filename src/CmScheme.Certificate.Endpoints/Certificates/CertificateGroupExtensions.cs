using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class CertificateGroupExtensions
{
    public static IEndpointRouteBuilder MapCertificateEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("certificates")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.Certificate, "Read", requireScope: false);

        return group;
    }
}
