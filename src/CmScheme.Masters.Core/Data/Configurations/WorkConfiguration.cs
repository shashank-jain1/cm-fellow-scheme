using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.HasKey(w => w.WorkId);

        builder.Property(w => w.WorkName)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(w => w.WorkDescription)
            .HasMaxLength(1000);

        builder.Property(w => w.Priority)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(w => w.AssignedTo)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(w => w.Remarks)
            .HasMaxLength(500);

        builder.Property(w => w.IsActive)
            .HasDefaultValue(true);

        builder.Property(w => w.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(w => w.ModifiedOn)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(w => w.ProjectId);

        builder.HasOne(w => w.Project)
            .WithMany()
            .HasForeignKey(w => w.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
