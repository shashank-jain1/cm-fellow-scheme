using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.ListSurveyRecords;

public sealed record ListSurveyRecordsQuery : IQuery<Result<IReadOnlyList<Core.Dtos.SurveyRecordDto>>>
{
    public int TaskProgressId { get; init; }
}
