using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkAllocationEntity = CmScheme.WorkAllocation.Core.Entities.WorkAllocation;

namespace CmScheme.WorkAllocation.Core.Data.Configurations;

public sealed class TaskDependencyConfiguration : IEntityTypeConfiguration<TaskDependency>
{
    public void Configure(EntityTypeBuilder<TaskDependency> builder)
    {
        builder.ToTable("TaskDependencies");
        builder.HasKey(e => e.TaskDependencyId);
        builder.HasOne<WorkAllocationEntity>()
            .WithMany()
            .HasForeignKey(e => e.WorkAllocationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<WorkAllocationEntity>()
            .WithMany()
            .HasForeignKey(e => e.DependsOnWorkAllocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
