using CmScheme.Endpoints.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.SurveyRecords;

public sealed class SurveyRecordEndpoints : IApiEndpoint
{
    public void Configure(IEndpointRouteBuilder builder)
    {
        builder.MapSurveyRecordEndpoints();
    }
}
