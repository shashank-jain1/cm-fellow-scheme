using Ardalis.Result;
using Mediator;
using Microsoft.AspNetCore.Http;
using CmScheme.WorkAllocation.Application.Features.TaskProgress.RecordSurveySubmission;
using CmScheme.Endpoints.Abstractions.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace CmScheme.WorkAllocation.Endpoints.TaskProgresses;

public sealed class RecordSurvey
{
    public static async Task<IResult> Handle(int taskProgressId, RecordSurveySubmissionCommand command, ISender sender, CancellationToken ct)
    {
        RecordSurveySubmissionCommand commandWithId = command with { TaskProgressId = taskProgressId };
        ValueTask<Result> result = sender.Send(commandWithId, ct);
        return await result.ToApiResultAsync();
    }
}
