using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Certificate.Core.Data.Configurations;

public class ExitRecordConfiguration : IEntityTypeConfiguration<ExitRecord>
{
    public void Configure(EntityTypeBuilder<ExitRecord> builder)
    {
        builder.ToTable("ExitRecords");
        builder.HasKey(e => e.ExitRecordId);
        builder.Property(e => e.CompletionStatus).HasMaxLength(50);
        builder.Property(e => e.VerificationFlags).HasMaxLength(500);
        builder.Property(e => e.ExitReportPath).HasMaxLength(500);
        builder.Property(e => e.Status).HasMaxLength(50);
        builder.Property(e => e.CreatedBy).HasMaxLength(200);
    }
}
