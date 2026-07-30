using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.MarkTrainingAttendance;

public sealed record MarkTrainingAttendanceCommand : ICommand<Result<bool>>
{
    public int TrainingEnrollmentId { get; init; }
}
