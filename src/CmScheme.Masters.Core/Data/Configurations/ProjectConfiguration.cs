using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");
        builder.HasKey(p => p.ProjectId);

        builder.Property(p => p.ProjectName)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(p => p.ProjectCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.ProjectDescription)
            .HasMaxLength(1000);

        builder.Property(p => p.DepartmentName)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.ProjectIncharge)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.BudgetAmount)
            .HasColumnType("decimal(15,2)");

        builder.Property(p => p.ProjectDocumentPath)
            .HasMaxLength(255);

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.ModifiedOn)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
