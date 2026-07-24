using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Registration.Endpoints.Registrations;
using CmScheme.Registration.Endpoints.UserAccounts;

namespace CmScheme.Registration.Endpoints;

public sealed class RegistrationEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapRegistrationEndpoints();
        builder.MapUserAccountEndpoints();
    }
}
