namespace CmScheme.Training.Core.Dtos;

public sealed record TrainingCompletionDto(
    int TrainingCompletionId,
    int TrainingScheduleId,
    int UserAccountId,
    string Status,
    DateTime? CompletedOn,
    bool CertificateIssued,
    int? FeedbackRating,
    string? FeedbackComments);
