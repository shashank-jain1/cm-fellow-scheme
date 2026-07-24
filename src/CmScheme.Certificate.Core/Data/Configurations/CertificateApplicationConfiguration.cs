using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Certificate.Core.Data.Configurations;

public class CertificateApplicationConfiguration : IEntityTypeConfiguration<CertificateApplication>
{
    public void Configure(EntityTypeBuilder<CertificateApplication> builder)
    {
        builder.HasKey(e => e.CertificateId);
        builder.Property(e => e.ApplicantName).HasMaxLength(200);
        builder.Property(e => e.ProgramName).HasMaxLength(200);
        builder.Property(e => e.VerifiedBy).HasMaxLength(200);
        builder.Property(e => e.DigitalSignaturePath).HasMaxLength(500);
        builder.Property(e => e.CertificatePdfPath).HasMaxLength(500);
        builder.Property(e => e.Status).HasMaxLength(50);
        builder.Property(e => e.CreatedBy).HasMaxLength(200);
    }
}
