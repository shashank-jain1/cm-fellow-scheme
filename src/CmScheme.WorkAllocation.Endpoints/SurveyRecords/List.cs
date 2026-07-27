using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.SurveyRecord.ListSurveyRecords;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.SurveyRecords;

public static class List
{
    public static async Task<IResult> Handle(int taskProgressId, ISender sender)
    {
        ListSurveyRecordsQuery query = new ListSurveyRecordsQuery { TaskProgressId = taskProgressId };
        Result<IReadOnlyList<Core.Dtos.SurveyRecordDto>> result = await sender.Send(query);
        return result.ToApiResult();
    }
}
