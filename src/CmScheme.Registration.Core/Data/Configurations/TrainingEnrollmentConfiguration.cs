using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class TrainingEnrollmentConfiguration : IEntityTypeConfiguration<TrainingEnrollment>
{
    public void Configure(EntityTypeBuilder<TrainingEnrollment> builder)
    {
        builder.ToTable("TrainingEnrollment");
        builder.HasKey(te => te.TrainingEnrollmentId);

        builder.Property(te => te.Status)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(te => te.EnrolledOn)
            .IsRequired();

        builder.HasIndex(te => new { te.TrainingScheduleId, te.UserAccountId })
            .IsUnique();
    }
}
