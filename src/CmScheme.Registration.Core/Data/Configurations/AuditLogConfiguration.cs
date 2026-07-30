using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLog");
        builder.HasKey(al => al.AuditLogId);

        builder.Property(al => al.Action)
            .HasMaxLength(50);

        builder.Property(al => al.EntityName)
            .HasMaxLength(100);

        builder.Property(al => al.EntityId)
            .HasMaxLength(50);

        builder.Property(al => al.OldValues)
            .HasColumnType("nvarchar(max)");

        builder.Property(al => al.NewValues)
            .HasColumnType("nvarchar(max)");

        builder.Property(al => al.IpAddress)
            .HasMaxLength(45);

        builder.Property(al => al.UserAgent)
            .HasMaxLength(500);

        builder.HasIndex(al => al.EntityName);
        builder.HasIndex(al => al.UserId);
        builder.HasIndex(al => al.CreatedOn);
    }
}
