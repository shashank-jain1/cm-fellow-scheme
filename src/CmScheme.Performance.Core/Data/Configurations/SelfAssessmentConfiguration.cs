using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Performance.Core.Data.Configurations;

public sealed class SelfAssessmentConfiguration : IEntityTypeConfiguration<SelfAssessment>
{
    public void Configure(EntityTypeBuilder<SelfAssessment> builder)
    {
        builder.ToTable("SelfAssessments");
        builder.HasKey(e => e.SelfAssessmentId);
        builder.Property(e => e.Strengths).HasMaxLength(1000);
        builder.Property(e => e.Improvements).HasMaxLength(1000);
        builder.Property(e => e.GoalsAchieved).HasMaxLength(1000);
        builder.Property(e => e.GoalsMissed).HasMaxLength(1000);
        builder.Property(e => e.TrainingFeedback).HasMaxLength(1000);
        builder.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Draft");
    }
}
