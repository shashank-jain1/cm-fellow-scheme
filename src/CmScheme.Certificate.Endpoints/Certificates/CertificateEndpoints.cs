using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.Certificate.Endpoints.Certificates;

public sealed class CertificateEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapCertificateEndpoints();
    }
}
