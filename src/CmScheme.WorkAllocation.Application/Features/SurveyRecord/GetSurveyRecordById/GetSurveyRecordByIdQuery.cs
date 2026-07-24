using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.GetSurveyRecordById;

public sealed record GetSurveyRecordByIdQuery(int SurveyRecordId) : IQuery<Result<Core.Dtos.SurveyRecordDto?>>;
