using Ardalis.Result;
using CmScheme.WorkAllocation.Application.Features.SurveyRecord.CreateSurveyRecord;
using CmScheme.Endpoints.Abstractions.Extensions;
using Mediator;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.SurveyRecords;

public static class Create
{
    public static async Task<IResult> Handle(CreateSurveyRecordCommand command, ISender sender)
    {
        Result<int> result = await sender.Send(command);
        return result.ToApiResult();
    }
}
