using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class ModuleAccessAuditLogConfiguration : IEntityTypeConfiguration<ModuleAccessAuditLog>
{
    public void Configure(EntityTypeBuilder<ModuleAccessAuditLog> builder)
    {
        builder.ToTable("ModuleAccessAuditLog");
        builder.HasKey(a => a.ModuleAccessAuditLogId);

        builder.Property(a => a.UserModuleAccessId)
            .IsRequired();

        builder.Property(a => a.UserAccountId)
            .IsRequired();

        builder.Property(a => a.ModuleMasterId)
            .IsRequired();

        builder.Property(a => a.Action)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.OldValues)
            .HasMaxLength(500);

        builder.Property(a => a.NewValues)
            .HasMaxLength(500);

        builder.Property(a => a.PerformedBy)
            .IsRequired();

        builder.Property(a => a.Reason)
            .HasMaxLength(500);

        builder.HasOne(a => a.UserAccount)
            .WithMany()
            .HasForeignKey(a => a.UserAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.ModuleMaster)
            .WithMany()
            .HasForeignKey(a => a.ModuleMasterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Performer)
            .WithMany()
            .HasForeignKey(a => a.PerformedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.UserAccountId);
        builder.HasIndex(a => a.ModuleMasterId);
        builder.HasIndex(a => a.PerformedOn);
    }
}
