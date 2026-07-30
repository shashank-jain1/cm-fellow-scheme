using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.GetTrainingEnrollments;

public sealed record GetTrainingEnrollmentsQuery : IQuery<Result<List<TrainingEnrollmentDto>>>
{
    public int TrainingScheduleId { get; init; }
}

public sealed record TrainingEnrollmentDto
{
    public int TrainingEnrollmentId { get; init; }
    public int TrainingScheduleId { get; init; }
    public int UserAccountId { get; init; }
    public string Status { get; init; } = null!;
    public bool AttendanceMarked { get; init; }
    public bool CertificateIssued { get; init; }
    public DateTime EnrolledOn { get; init; }
    public DateTime? CompletedOn { get; init; }
}
