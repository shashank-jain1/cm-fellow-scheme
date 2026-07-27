using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.SurveyRecord.CreateSurveyRecord;

public sealed record CreateSurveyRecordCommand : ICommand<Result<int>>
{
    public int TaskProgressId { get; init; }
    public string InternName { get; init; } = null!;
    public string SurveyPersonName { get; init; } = null!;
    public string MobileNumber { get; init; } = null!;
    public string PanchayatName { get; init; } = null!;
    public string VillageName { get; init; } = null!;
    public DateTime SurveyDate { get; init; }
    public string SurveyStatus { get; init; } = null!;
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
}
