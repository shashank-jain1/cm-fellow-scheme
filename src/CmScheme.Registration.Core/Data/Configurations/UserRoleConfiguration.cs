using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        builder.HasKey(ur => ur.UserRoleId);

        builder.Property(ur => ur.UserAccountId)
            .IsRequired();

        builder.Property(ur => ur.RoleLookupId)
            .IsRequired();

        builder.HasOne(ur => ur.UserAccount)
            .WithMany()
            .HasForeignKey(ur => ur.UserAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ur => new { ur.UserAccountId, ur.RoleLookupId })
            .IsUnique();
    }
}
