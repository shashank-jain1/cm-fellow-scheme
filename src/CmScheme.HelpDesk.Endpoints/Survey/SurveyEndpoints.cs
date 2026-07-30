using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.HelpDesk.Endpoints.Survey;

public sealed class SurveyEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapSurveyEndpoints();
    }
}
