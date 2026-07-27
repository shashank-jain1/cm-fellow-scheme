using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Performance.Core.Data.Configurations;

public class PerformanceEvaluationConfiguration : IEntityTypeConfiguration<PerformanceEvaluation>
{
    public void Configure(EntityTypeBuilder<PerformanceEvaluation> builder)
    {
        builder.ToTable("PerformanceEvaluations");
        builder.HasKey(e => e.PerformanceEvaluationId);
        builder.Property(e => e.ProjectName).HasMaxLength(200);
        builder.Property(e => e.WorkProject).HasMaxLength(200);
        builder.Property(e => e.WorkDescription).HasMaxLength(1000);
        builder.Property(e => e.ApplicantNumber).HasMaxLength(100);
        builder.Property(e => e.ApplicantName).HasMaxLength(200);
        builder.Property(e => e.AssignedWork).HasMaxLength(500);
        builder.Property(e => e.WorkingLocation).HasMaxLength(200);
        builder.Property(e => e.PerformanceGrade).HasMaxLength(10);
        builder.Property(e => e.PerformanceStatus).HasMaxLength(50);
        builder.Property(e => e.EvaluationRemarks).HasMaxLength(2000);
        builder.Property(e => e.EvaluatedBy).HasMaxLength(200);
    }
}
