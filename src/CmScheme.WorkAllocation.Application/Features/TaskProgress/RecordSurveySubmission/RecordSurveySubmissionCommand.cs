using Ardalis.Result;
using Mediator;

namespace CmScheme.WorkAllocation.Application.Features.TaskProgress.RecordSurveySubmission;

public sealed record RecordSurveySubmissionCommand : ICommand<Result>
{
    public int TaskProgressId { get; init; }
    public int ApplicantId { get; init; }
    public string SurveyPersonName { get; init; } = null!;
    public string MobileNumber { get; init; } = null!;
    public string PanchayatName { get; init; } = null!;
    public string VillageName { get; init; } = null!;
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
}
