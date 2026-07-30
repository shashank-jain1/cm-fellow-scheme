using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using CmScheme.Endpoints.Abstractions.Authorization;

namespace CmScheme.WorkAllocation.Endpoints.SurveyRecords;

public static class SurveyRecordGroupExtensions
{
    public static IEndpointRouteBuilder MapSurveyRecordEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("survey-records")
            .RequireAuthorization()
            .RequireModule(ModuleCodes.WorkAllocation, "Read", requireScope: false);

        group.MapPost("/", Create.Handle);
        group.MapGet("/{surveyRecordId:int}", Get.Handle);
        group.MapGet("/by-task-progress/{taskProgressId:int}", List.Handle);

        return builder;
    }
}
