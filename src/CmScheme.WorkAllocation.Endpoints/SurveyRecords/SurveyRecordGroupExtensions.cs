using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CmScheme.WorkAllocation.Endpoints.SurveyRecords;

public static class SurveyRecordGroupExtensions
{
    public static IEndpointRouteBuilder MapSurveyRecordEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("survey-records");

        group.MapPost("/", Create.Handle);
        group.MapGet("/{surveyRecordId:int}", Get.Handle);
        group.MapGet("/by-task-progress/{taskProgressId:int}", List.Handle);

        return builder;
    }
}
