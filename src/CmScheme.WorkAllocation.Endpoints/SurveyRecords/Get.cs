using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.SurveyRecord.GetSurveyRecordById;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.SurveyRecords;

public static class Get
{
    public static async Task<IResult> Handle(int surveyRecordId, ISender sender)
    {
        GetSurveyRecordByIdQuery query = new GetSurveyRecordByIdQuery(surveyRecordId);
        Result<Core.Dtos.SurveyRecordDto?> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
