using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.MarkAttendance;

public sealed record MarkAttendanceCommand : ICommand<Result<bool>>
{
    public int TrainingEnrollmentId { get; init; }
    public bool Marked { get; init; }
}
