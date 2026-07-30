using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using Entity = CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.EnrollTrainee;

public sealed class EnrollTraineeCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<EnrollTraineeCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        EnrollTraineeCommand request,
        CancellationToken cancellationToken)
    {
        bool alreadyEnrolled = await dbContext.TrainingEnrollments
            .AnyAsync(te =>
                te.TrainingScheduleId == request.TrainingScheduleId &&
                te.UserAccountId == request.UserAccountId, cancellationToken);

        if (alreadyEnrolled)
        {
            return Result<int>.Conflict("Fellow is already enrolled in this training.");
        }

        Entity.TrainingEnrollment enrollment = new()
        {
            TrainingScheduleId = request.TrainingScheduleId,
            UserAccountId = request.UserAccountId,
            Status = "Enrolled",
            AttendanceMarked = false,
            CertificateIssued = false,
            EnrolledOn = DateTime.UtcNow,
            CreatedOn = DateTime.UtcNow,
        };

        dbContext.TrainingEnrollments.Add(enrollment);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(enrollment.TrainingEnrollmentId);
    }
}
