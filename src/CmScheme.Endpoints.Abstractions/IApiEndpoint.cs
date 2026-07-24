using Microsoft.AspNetCore.Routing;

namespace CmScheme.Endpoints.Abstractions;

public interface IApiEndpoint
{
    void Configure(IEndpointRouteBuilder builder);
}
