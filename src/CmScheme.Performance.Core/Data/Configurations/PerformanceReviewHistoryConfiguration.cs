using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Performance.Core.Entities;

namespace CmScheme.Performance.Core.Data.Configurations;

public class PerformanceReviewHistoryConfiguration : IEntityTypeConfiguration<PerformanceReviewHistory>
{
    public void Configure(EntityTypeBuilder<PerformanceReviewHistory> builder)
    {
        builder.ToTable("PerformanceReviewHistories");
        builder.HasKey(e => e.PerformanceReviewHistoryId);
        builder.Property(e => e.Action).HasMaxLength(20);
        builder.Property(e => e.PreviousLevel).HasMaxLength(20);
        builder.Property(e => e.NewLevel).HasMaxLength(20);
        builder.Property(e => e.PreviousStatus).HasMaxLength(20);
        builder.Property(e => e.NewStatus).HasMaxLength(20);
        builder.Property(e => e.PerformedBy).HasMaxLength(200);
        builder.Property(e => e.Remarks).HasMaxLength(2000);
        builder.HasOne<PerformanceEvaluation>()
            .WithMany()
            .HasForeignKey(e => e.PerformanceEvaluationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
