using CmScheme.Performance.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.Performance.Core.Data.Configurations;

public sealed class ImprovementPlanConfiguration : IEntityTypeConfiguration<ImprovementPlan>
{
    public void Configure(EntityTypeBuilder<ImprovementPlan> builder)
    {
        builder.ToTable("ImprovementPlans");
        builder.HasKey(e => e.ImprovementPlanId);
        builder.Property(e => e.PlanTitle).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(1000);
        builder.Property(e => e.Status).HasMaxLength(20).IsRequired();
    }
}
