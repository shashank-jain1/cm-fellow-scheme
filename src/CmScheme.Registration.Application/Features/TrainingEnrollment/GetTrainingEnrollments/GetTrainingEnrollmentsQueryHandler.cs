using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;

namespace CmScheme.Registration.Application.Features.TrainingEnrollment.GetTrainingEnrollments;

public sealed class GetTrainingEnrollmentsQueryHandler(IRegistrationCommandDbContext dbContext)
    : IQueryHandler<GetTrainingEnrollmentsQuery, Result<List<TrainingEnrollmentDto>>>
{
    public async ValueTask<Result<List<TrainingEnrollmentDto>>> Handle(
        GetTrainingEnrollmentsQuery request,
        CancellationToken cancellationToken)
    {
        List<TrainingEnrollmentDto> enrollments = await dbContext.TrainingEnrollments
            .Where(te => te.TrainingScheduleId == request.TrainingScheduleId)
            .Select(te => new TrainingEnrollmentDto
            {
                TrainingEnrollmentId = te.TrainingEnrollmentId,
                TrainingScheduleId = te.TrainingScheduleId,
                UserAccountId = te.UserAccountId,
                Status = te.Status,
                AttendanceMarked = te.AttendanceMarked,
                CertificateIssued = te.CertificateIssued,
                EnrolledOn = te.EnrolledOn,
                CompletedOn = te.CompletedOn,
            })
            .ToListAsync(cancellationToken);

        return Result<List<TrainingEnrollmentDto>>.Success(enrollments);
    }
}
