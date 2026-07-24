using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class Applicant
{
    [Key]
    public int ApplicantId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string FatherName { get; set; } = null!;

    [MaxLength(12)]
    public string? AadhaarNumber { get; set; }

    [MaxLength(10)]
    public string? PANNumber { get; set; }

    [MaxLength(20)]
    public string? DrivingLicenseNumber { get; set; }

    [MaxLength(9)]
    public string? SamagraId { get; set; }

    [Required]
    [MaxLength(10)]
    public string MobileNumber { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string EmailId { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(500)]
    public string PermanentAddress { get; set; } = null!;

    public int DivisionId { get; set; }

    public int DistrictId { get; set; }

    public int BlockId { get; set; }

    public int GramPanchayatId { get; set; }

    [MaxLength(6)]
    public string PinCode { get; set; } = null!;

    public int AppliedForTraining { get; set; }

    public int PreferredTrainingLocationId { get; set; }

    public int QualificationId { get; set; }

    [MaxLength(200)]
    public string BoardUniversityName { get; set; } = null!;

    public int PassingYear { get; set; }

    public decimal PercentageCGPA { get; set; }

    [MaxLength(1000)]
    public string? ExperienceDetails { get; set; }

    [MaxLength(255)]
    public string? PhotographPath { get; set; }

    [MaxLength(255)]
    public string? IdentityProofPath { get; set; }

    [MaxLength(255)]
    public string? EducationalCertificatePath { get; set; }

    public bool DeclarationAccepted { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Pending";

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

    public int? ModifiedBy { get; set; }
}
