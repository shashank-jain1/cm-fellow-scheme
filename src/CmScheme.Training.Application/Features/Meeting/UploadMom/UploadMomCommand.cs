using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Meeting.UploadMom;

public sealed record UploadMomCommand : ICommand<Result<string>>
{
    public int TrainingScheduleId { get; init; }
    public string FileName { get; init; } = null!;
    public Stream FileStream { get; init; } = null!;
}
