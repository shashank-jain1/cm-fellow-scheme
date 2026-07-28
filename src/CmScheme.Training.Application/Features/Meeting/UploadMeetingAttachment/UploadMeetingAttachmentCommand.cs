using Ardalis.Result;
using Mediator;

namespace CmScheme.Training.Application.Features.Meeting.UploadMeetingAttachment;

public sealed record UploadMeetingAttachmentCommand : ICommand<Result<string>>
{
    public int TrainingScheduleId { get; init; }
    public string FileName { get; init; } = null!;
    public Stream FileStream { get; init; } = null!;
}
