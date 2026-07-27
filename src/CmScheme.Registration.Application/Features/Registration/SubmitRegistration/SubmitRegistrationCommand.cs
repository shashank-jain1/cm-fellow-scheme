using Ardalis.Result;
using Mediator;

namespace CmScheme.Registration.Application.Features.Registration.SubmitRegistration;

public sealed record SubmitRegistrationCommand : ICommand<Result<int>>
{
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = null!;
    public string FatherName { get; init; } = null!;
    public string? AadhaarNumber { get; init; }
    public string? PanNumber { get; init; }
    public string? DrivingLicenseNumber { get; init; }
    public string? SamagraId { get; init; }
    public string MobileNumber { get; init; } = null!;
    public string EmailId { get; init; } = null!;
    public DateTime DateOfBirth { get; init; }
    public string PermanentAddress { get; init; } = null!;
    public int DivisionId { get; init; }
    public int DistrictId { get; init; }
    public int BlockId { get; init; }
    public int GramPanchayatId { get; init; }
    public string PinCode { get; init; } = null!;
    public int AppliedForTraining { get; init; }
    public int PreferredTrainingLocationId { get; init; }
    public int QualificationId { get; init; }
    public string BoardUniversityName { get; init; } = null!;
    public int PassingYear { get; init; }
    public decimal PercentageCGPA { get; init; }
    public string? ExperienceDetails { get; init; }
    public string? PhotographPath { get; init; }
    public string? IdentityProofPath { get; init; }
    public string? EducationalCertificatePath { get; init; }
    public bool DeclarationAccepted { get; init; }
}
