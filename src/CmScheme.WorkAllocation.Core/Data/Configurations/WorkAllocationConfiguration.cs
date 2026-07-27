using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;

namespace CmScheme.WorkAllocation.Core.Data.Configurations;

public sealed class WorkAllocationConfiguration : IEntityTypeConfiguration<WorkAllocationEntity>
{
    public void Configure(EntityTypeBuilder<WorkAllocationEntity> builder)
    {
        builder.ToTable("WorkAllocations");
        builder.HasKey(e => e.WorkAllocationId);
        builder.Property(e => e.WorkProjectId).HasMaxLength(50);
        builder.Property(e => e.WorkDescription).HasMaxLength(2000);
        builder.Property(e => e.Priority).HasMaxLength(20);
        builder.Property(e => e.Status).HasMaxLength(50);
        builder.Property(e => e.CreatedBy).HasMaxLength(200);
        builder.Property(e => e.ModifiedBy).HasMaxLength(200);
    }
}
