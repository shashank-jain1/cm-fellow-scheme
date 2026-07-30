using Ardalis.Result;
using Mediator;
using Microsoft.EntityFrameworkCore;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.UpdateProfile;

public sealed class UpdateProfileCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<UpdateProfileCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateProfileCommand request,
        CancellationToken cancellationToken)
    {
        Applicant? applicant = await dbContext.Applicants
            .FirstOrDefaultAsync(a => a.ApplicantId == request.ApplicantId, cancellationToken);

        if (applicant is null)
        {
            return Result.NotFound("Applicant not found.");
        }

        applicant.FirstName = request.FirstName;
        applicant.MiddleName = request.MiddleName;
        applicant.LastName = request.LastName;
        applicant.MobileNumber = request.MobileNumber;
        applicant.EmailId = request.EmailId;
        applicant.PermanentAddress = request.PermanentAddress;
        applicant.QualificationId = request.QualificationId;
        applicant.ExperienceDetails = request.ExperienceDetails;
        applicant.ModifiedOn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
