using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Certificate.Endpoints.Certificates;

public static class CertificateGroupExtensions
{
    public static IEndpointRouteBuilder MapCertificateEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("certificates");

        group.MapGet("/", List.Handle);
        group.MapPost("/", Apply.Handle);
        group.MapPut("/review", Review.Handle);
        group.MapGet("/status", GetStatus.Handle);
        group.MapPost("/{certificateId:int}/generate", Generate.Handle);
        group.MapGet("/{certificateId:int}/download", Download.Handle);

        return builder;
    }
}
