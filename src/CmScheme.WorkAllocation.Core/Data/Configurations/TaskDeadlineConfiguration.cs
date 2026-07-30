using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.WorkAllocation.Core.Data.Configurations;

public sealed class TaskDeadlineConfiguration : IEntityTypeConfiguration<TaskDeadline>
{
    public void Configure(EntityTypeBuilder<TaskDeadline> builder)
    {
        builder.ToTable("TaskDeadlines");
        builder.HasKey(e => e.TaskDeadlineId);
        builder.Property(e => e.ReminderDaysBefore).HasDefaultValue(3);
        builder.Property(e => e.IsOverdue).HasDefaultValue(false);
    }
}
