using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using Entity = CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.MarkAttendance;

public sealed class MarkAttendanceCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<MarkAttendanceCommand, Result<bool>>
{
    public async ValueTask<Result<bool>> Handle(
        MarkAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        Entity.TrainingEnrollment? enrollment = await dbContext.TrainingEnrollments
            .FirstOrDefaultAsync(te => te.TrainingEnrollmentId == request.TrainingEnrollmentId, cancellationToken);

        if (enrollment is null)
        {
            return Result<bool>.NotFound("Training enrollment not found.");
        }

        enrollment.AttendanceMarked = request.Marked;

        if (request.Marked && enrollment.Status == "Enrolled")
        {
            enrollment.Status = "Completed";
            enrollment.CompletedOn = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(enrollment.AttendanceMarked);
    }
}
