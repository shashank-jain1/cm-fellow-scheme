using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class UserModuleAccessConfiguration : IEntityTypeConfiguration<UserModuleAccess>
{
    public void Configure(EntityTypeBuilder<UserModuleAccess> builder)
    {
        builder.ToTable("UserModuleAccess");
        builder.HasKey(uma => uma.UserModuleAccessId);

        builder.Property(uma => uma.UserAccountId)
            .IsRequired();

        builder.Property(uma => uma.ModuleMasterId)
            .IsRequired();

        builder.Property(uma => uma.DivisionId)
            .IsRequired(false);

        builder.Property(uma => uma.DistrictId)
            .IsRequired(false);

        builder.Property(uma => uma.BlockId)
            .IsRequired(false);

        builder.HasOne(uma => uma.UserAccount)
            .WithMany()
            .HasForeignKey(uma => uma.UserAccountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uma => uma.ModuleMaster)
            .WithMany()
            .HasForeignKey(uma => uma.ModuleMasterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(uma => new
        {
            uma.UserAccountId,
            uma.ModuleMasterId,
            uma.DivisionId,
            uma.DistrictId,
            uma.BlockId
        })
            .IsUnique();
    }
}
