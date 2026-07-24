using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.ListSurveyRecords;

public sealed record ListSurveyRecordsQuery(int TaskProgressId) : IQuery<Result<IReadOnlyList<Core.Dtos.SurveyRecordDto>>>;
