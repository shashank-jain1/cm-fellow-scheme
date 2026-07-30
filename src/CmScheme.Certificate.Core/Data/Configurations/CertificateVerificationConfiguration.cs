using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Certificate.Core.Data.Configurations;

public class CertificateVerificationConfiguration : IEntityTypeConfiguration<CertificateVerification>
{
    public void Configure(EntityTypeBuilder<CertificateVerification> builder)
    {
        builder.ToTable("CertificateVerifications");
        builder.HasKey(e => e.CertificateVerificationId);
        builder.Property(e => e.CertificateNumber).HasMaxLength(100);
        builder.Property(e => e.FellowName).HasMaxLength(200);
        builder.Property(e => e.ProgramName).HasMaxLength(200);
        builder.Property(e => e.VerificationUrl).HasMaxLength(500);
        builder.Property(e => e.QrCodeData).HasMaxLength(1000);
        builder.Property(e => e.Status).HasMaxLength(50);
        builder.Property(e => e.CreatedBy).HasMaxLength(200);
        builder.HasIndex(e => e.CertificateNumber).IsUnique();
    }
}
