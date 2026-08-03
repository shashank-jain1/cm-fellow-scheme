using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.RejectRegistration;

public sealed class RejectRegistrationCommandHandler(
    IRegistrationCommandDbContext dbContext,
    INotificationService notificationService)
    : ICommandHandler<RejectRegistrationCommand, Result>
{
    public async ValueTask<Result> Handle(
        RejectRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(a => a.ApplicantId == request.ApplicantId, cancellationToken);

        if (applicant is null)
        {
            return Result.NotFound("Applicant not found.");
        }

        if (applicant.Status == Statuses.Registration.Rejected)
        {
            return Result.Conflict("Registration is already rejected.");
        }

        applicant.Status = Statuses.Registration.Rejected;
        applicant.ModifiedOn = DateTime.UtcNow;
        applicant.ModifiedBy = request.RejectedBy;

        await dbContext.SaveChangesAsync(cancellationToken);

        await notificationService.SendEmailAsync(
            applicant.EmailId,
            "Registration Rejected - CM Fellow Program",
            $"Dear {applicant.FirstName} {applicant.LastName},\n\n" +
            $"We regret to inform you that your registration for the CM Fellow Program has been rejected.\n\n" +
            $"If you have any questions, please contact the administrator.\n\n" +
            $"Best regards,\nCM Fellow Program Team",
            cancellationToken);

        return Result.NoContent();
    }
}
