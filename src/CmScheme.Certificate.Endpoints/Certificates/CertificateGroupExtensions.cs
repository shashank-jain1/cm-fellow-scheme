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

        group.MapPost("/", Apply.Handle).DisableAntiforgery();
        group.MapGet("/", List.Handle);
        group.MapPut("/review", Review.Handle).DisableAntiforgery();
        group.MapPost("/{certificateId:int}/generate", Generate.Handle).DisableAntiforgery();
        group.MapGet("/{certificateId:int}/download", Download.Handle);
        group.MapGet("/verify/{certificateNumber}", VerifyCertificate.Handle);
        group.MapPost("/{applicantId:int}/completion-certificate", GenerateCompletionCertificate.Handle).DisableAntiforgery();
        group.MapPost("/{applicantId:int}/experience-letter", GenerateExperienceLetter.Handle).DisableAntiforgery();
        group.MapGet("/status", GetStatus.Handle);

        return group;
    }
}
