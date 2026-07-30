using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using Entity = CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.MarkTrainingAttendance;

public sealed class MarkTrainingAttendanceCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<MarkTrainingAttendanceCommand, Result<bool>>
{
    public async ValueTask<Result<bool>> Handle(
        MarkTrainingAttendanceCommand request,
        CancellationToken cancellationToken)
    {
        Entity.TrainingEnrollment? enrollment = await dbContext.TrainingEnrollments
            .FirstOrDefaultAsync(te => te.TrainingEnrollmentId == request.TrainingEnrollmentId, cancellationToken);

        if (enrollment is null)
        {
            return Result<bool>.NotFound("Training enrollment not found.");
        }

        enrollment.AttendanceMarked = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(enrollment.AttendanceMarked);
    }
}
