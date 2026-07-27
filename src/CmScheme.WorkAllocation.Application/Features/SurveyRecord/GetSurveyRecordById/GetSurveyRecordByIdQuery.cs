using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.GetSurveyRecordById;

public sealed record GetSurveyRecordByIdQuery : IQuery<Result<Core.Dtos.SurveyRecordDto?>>
{
    public int SurveyRecordId { get; init; }
}
