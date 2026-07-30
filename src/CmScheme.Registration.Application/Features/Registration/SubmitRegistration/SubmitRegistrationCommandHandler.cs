using Ardalis.Result;
using Mediator;
using CmScheme.Common.Core;
using CmScheme.Common.Core.Services;
using CmScheme.Registration.Core.Data;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Application.Features.Registration.SubmitRegistration;

public sealed class SubmitRegistrationCommandHandler(
    IRegistrationCommandDbContext dbContext,
    IFileUploadService fileUploadService)
    : ICommandHandler<SubmitRegistrationCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(
        SubmitRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        string? photographPath = null;
        if (request.Photograph is { Length: > 0 })
        {
            using var stream = request.Photograph.OpenReadStream();
            photographPath = await fileUploadService.UploadAsync(
                stream,
                request.Photograph.FileName,
                request.Photograph.ContentType,
                "documents/photographs",
                cancellationToken);
        }

        string? identityProofPath = null;
        if (request.IdentityProof is { Length: > 0 })
        {
            using var stream = request.IdentityProof.OpenReadStream();
            identityProofPath = await fileUploadService.UploadAsync(
                stream,
                request.IdentityProof.FileName,
                request.IdentityProof.ContentType,
                "documents/identity",
                cancellationToken);
        }

        string? educationalCertificatePath = null;
        if (request.EducationalCertificate is { Length: > 0 })
        {
            using var stream = request.EducationalCertificate.OpenReadStream();
            educationalCertificatePath = await fileUploadService.UploadAsync(
                stream,
                request.EducationalCertificate.FileName,
                request.EducationalCertificate.ContentType,
                "documents/education",
                cancellationToken);
        }

        Applicant applicant = new Applicant
        {
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            FatherName = request.FatherName,
            AadhaarNumber = request.AadhaarNumber,
            PanNumber = request.PanNumber,
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
            PhotographPath = photographPath,
            IdentityProofPath = identityProofPath,
            EducationalCertificatePath = educationalCertificatePath,
            DeclarationAccepted = request.DeclarationAccepted,
            Status = Statuses.Registration.Pending,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow
        };

        dbContext.Applicants.Add(applicant);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(applicant.ApplicantId);
    }
}
