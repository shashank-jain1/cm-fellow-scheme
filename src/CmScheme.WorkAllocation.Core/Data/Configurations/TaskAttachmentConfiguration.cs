using CmScheme.WorkAllocation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmScheme.WorkAllocation.Core.Data.Configurations;

public sealed class TaskAttachmentConfiguration : IEntityTypeConfiguration<TaskAttachment>
{
    public void Configure(EntityTypeBuilder<TaskAttachment> builder)
    {
        builder.ToTable("TaskAttachments");
        builder.HasKey(e => e.TaskAttachmentId);
        builder.Property(e => e.FileName).HasMaxLength(255);
        builder.Property(e => e.FilePath).HasMaxLength(500);
        builder.Property(e => e.ContentType).HasMaxLength(100);
    }
}
