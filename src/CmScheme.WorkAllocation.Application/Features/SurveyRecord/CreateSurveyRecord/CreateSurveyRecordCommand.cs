using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.CreateSurveyRecord;

public sealed record CreateSurveyRecordCommand(
    int TaskProgressId,
    string InternName,
    string SurveyPersonName,
    string MobileNumber,
    string PanchayatName,
    string VillageName,
    DateTime SurveyDate,
    string SurveyStatus,
    decimal? Latitude,
    decimal? Longitude
) : ICommand<Result<int>>;
