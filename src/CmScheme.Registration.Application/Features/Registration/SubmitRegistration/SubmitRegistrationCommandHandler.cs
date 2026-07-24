using Ardalis.Result;
using Mediator;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.SubmitRegistration;

public sealed class SubmitRegistrationCommandHandler(IRegistrationCommandDbContext dbContext)
    : ICommandHandler<SubmitRegistrationCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        SubmitRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        Applicant applicant = new Applicant
        {
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            FatherName = request.FatherName,
            AadhaarNumber = request.AadhaarNumber,
            PANNumber = request.PANNumber,
            DrivingLicenseNumber = request.DrivingLicenseNumber,
            SamagraId = request.SamagraId,
            MobileNumber = request.MobileNumber,
            EmailId = request.EmailId,
            DateOfBirth = request.DateOfBirth,
            PermanentAddress = request.PermanentAddress,
            DivisionId = request.DivisionId,
            DistrictId = request.DistrictId,
            BlockId = request.BlockId,
            GramPanchayatId = request.GramPanchayatId,
            PinCode = request.PinCode,
            AppliedForTraining = request.AppliedForTraining,
            PreferredTrainingLocationId = request.PreferredTrainingLocationId,
            QualificationId = request.QualificationId,
            BoardUniversityName = request.BoardUniversityName,
            PassingYear = request.PassingYear,
            PercentageCGPA = request.PercentageCGPA,
            ExperienceDetails = request.ExperienceDetails,
            PhotographPath = request.PhotographPath,
            IdentityProofPath = request.IdentityProofPath,
            EducationalCertificatePath = request.EducationalCertificatePath,
            DeclarationAccepted = request.DeclarationAccepted,
            Status = "Pending",
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow
        };

        dbContext.Applicants.Add(applicant);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(applicant.ApplicantId);
    }
}
