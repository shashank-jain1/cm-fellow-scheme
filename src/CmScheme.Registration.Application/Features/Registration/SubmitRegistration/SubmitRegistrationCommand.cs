using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.SubmitRegistration;

public sealed record SubmitRegistrationCommand(
    string FirstName,
    string? MiddleName,
    string LastName,
    string FatherName,
    string? AadhaarNumber,
    string? PANNumber,
    string? DrivingLicenseNumber,
    string? SamagraId,
    string MobileNumber,
    string EmailId,
    DateTime DateOfBirth,
    string PermanentAddress,
    int DivisionId,
    int DistrictId,
    int BlockId,
    int GramPanchayatId,
    string PinCode,
    int AppliedForTraining,
    int PreferredTrainingLocationId,
    int QualificationId,
    string BoardUniversityName,
    int PassingYear,
    decimal PercentageCGPA,
    string? ExperienceDetails,
    string? PhotographPath,
    string? IdentityProofPath,
    string? EducationalCertificatePath,
    bool DeclarationAccepted)     : ICommand<Result<int>>;
