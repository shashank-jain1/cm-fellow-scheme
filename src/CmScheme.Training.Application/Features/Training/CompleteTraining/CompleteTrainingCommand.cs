using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Training.CompleteTraining;

public sealed record CompleteTrainingCommand : ICommand<Result<int>>
{
    public int TrainingScheduleId { get; init; }
    public int UserAccountId { get; init; }
    public bool CertificateIssued { get; init; }
    public int? FeedbackRating { get; init; }
    public string? FeedbackComments { get; init; }
}
