using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions;
using CmScheme.Registration.Endpoints.Auth;
using CmScheme.Registration.Endpoints.BulkImport;
using CmScheme.Registration.Endpoints.ExitInterview;
using CmScheme.Registration.Endpoints.Notifications;
using CmScheme.Registration.Endpoints.Registrations;
using CmScheme.Registration.Endpoints.UserAccounts;
using CmScheme.Registration.Endpoints.UserModuleAccess;

namespace CmScheme.Registration.Endpoints;

public sealed class RegistrationEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapAuthEndpoints();
        builder.MapRegistrationEndpoints();
        builder.MapUserAccountEndpoints();
        builder.MapUserModuleAccessEndpoints();
        builder.MapExitInterviewEndpoints();
        builder.MapBulkImportEndpoints();
        builder.MapNotificationEndpoints();
    }
}
