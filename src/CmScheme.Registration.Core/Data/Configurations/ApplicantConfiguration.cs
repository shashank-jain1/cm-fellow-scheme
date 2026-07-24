using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasKey(a => a.ApplicantId);

        builder.Property(a => a.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.MiddleName)
            .HasMaxLength(100);

        builder.Property(a => a.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.FatherName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(a => a.AadhaarNumber)
            .HasMaxLength(12);

        builder.Property(a => a.PANNumber)
            .HasMaxLength(10);

        builder.Property(a => a.DrivingLicenseNumber)
            .HasMaxLength(20);

        builder.Property(a => a.SamagraId)
            .HasMaxLength(9);

        builder.Property(a => a.MobileNumber)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(a => a.EmailId)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(a => a.PermanentAddress)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.PinCode)
            .HasMaxLength(6)
            .IsRequired();

        builder.Property(a => a.BoardUniversityName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.ExperienceDetails)
            .HasMaxLength(1000);

        builder.Property(a => a.PhotographPath)
            .HasMaxLength(255);

        builder.Property(a => a.IdentityProofPath)
            .HasMaxLength(255);

        builder.Property(a => a.EducationalCertificatePath)
            .HasMaxLength(255);

        builder.Property(a => a.Status)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.PercentageCGPA)
            .HasPrecision(5, 2);
    }
}
