using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Performance.Core.Data.Configurations;

public sealed class PerformanceReviewCycleConfiguration : IEntityTypeConfiguration<PerformanceReviewCycle>
{
    public void Configure(EntityTypeBuilder<PerformanceReviewCycle> builder)
    {
        builder.ToTable("PerformanceReviewCycles");
        builder.HasKey(e => e.ReviewCycleId);
        builder.Property(e => e.CycleName).HasMaxLength(100).IsRequired();
        builder.Property(e => e.IsActive).HasDefaultValue(true);
    }
}
