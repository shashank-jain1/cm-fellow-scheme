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

        group.MapGet("/", List.Handle);
        group.MapPost("/", Apply.Handle);
        group.MapPut("/review", Review.Handle);
        group.MapGet("/status", GetStatus.Handle);
        group.MapPost("/{certificateId:int}/generate", Generate.Handle);
        group.MapGet("/{certificateId:int}/download", Download.Handle);
        group.MapPost("/{applicantId:int}/completion-certificate", GenerateCompletionCertificate.Handle);
        group.MapPost("/{applicantId:int}/experience-letter", GenerateExperienceLetter.Handle);
        group.MapGet("/verify/{certificateNumber}", VerifyCertificate.Handle);

        return builder;
    }
}
