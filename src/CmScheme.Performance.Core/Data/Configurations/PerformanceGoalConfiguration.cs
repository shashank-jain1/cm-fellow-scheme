using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Performance.Core.Data.Configurations;

public sealed class PerformanceGoalConfiguration : IEntityTypeConfiguration<PerformanceGoal>
{
    public void Configure(EntityTypeBuilder<PerformanceGoal> builder)
    {
        builder.ToTable("PerformanceGoals");
        builder.HasKey(e => e.PerformanceGoalId);
        builder.Property(e => e.GoalTitle).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(1000);
        builder.Property(e => e.Status).HasMaxLength(20).IsRequired();
    }
}
