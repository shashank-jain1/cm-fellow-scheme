using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.WorkAllocation.Core.Data.Configurations;

public sealed class TaskProgressConfiguration : IEntityTypeConfiguration<TaskProgress>
{
    public void Configure(EntityTypeBuilder<TaskProgress> builder)
    {
        builder.ToTable("TaskProgresses");
        builder.HasKey(e => e.TaskProgressId);
        builder.Property(e => e.ProjectName).HasMaxLength(200);
        builder.Property(e => e.WorkProject).HasMaxLength(200);
        builder.Property(e => e.WorkDescription).HasMaxLength(2000);
        builder.Property(e => e.Priority).HasMaxLength(20);
        builder.Property(e => e.WorkStatus).HasMaxLength(50);
        builder.Property(e => e.CompletionPercentage).HasPrecision(5, 2);
        builder.Property(e => e.ProgressNotes).HasMaxLength(500);
        builder.Property(e => e.FellowProgressStatus).HasMaxLength(50);
    }
}
