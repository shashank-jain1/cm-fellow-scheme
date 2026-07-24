using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class CertificateGroupExtensions
{
    public static IEndpointRouteBuilder MapCertificateEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("certificates");

        group.MapPost("/", Apply.Handle);
        group.MapPut("/review", Review.Handle);
        group.MapGet("/status", GetStatus.Handle);

        return builder;
    }
}
