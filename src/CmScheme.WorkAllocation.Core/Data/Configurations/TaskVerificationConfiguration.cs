using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.WorkAllocation.Core.Data.Configurations;

public sealed class TaskVerificationConfiguration : IEntityTypeConfiguration<TaskVerification>
{
    public void Configure(EntityTypeBuilder<TaskVerification> builder)
    {
        builder.ToTable("TaskVerifications");
        builder.HasKey(e => e.TaskVerificationId);
        builder.Property(e => e.VerificationStatus).HasMaxLength(50);
        builder.Property(e => e.Comments).HasMaxLength(500);
    }
}
