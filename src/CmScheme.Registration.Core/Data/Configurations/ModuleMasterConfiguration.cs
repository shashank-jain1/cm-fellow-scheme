using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class ModuleMasterConfiguration : IEntityTypeConfiguration<ModuleMaster>
{
    public void Configure(EntityTypeBuilder<ModuleMaster> builder)
    {
        builder.ToTable("ModuleMaster");
        builder.HasKey(mm => mm.ModuleMasterId);

        builder.Property(mm => mm.ModuleCode)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(mm => mm.ModuleName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(mm => mm.Description)
            .HasMaxLength(500);

        builder.HasIndex(mm => mm.ModuleCode)
            .IsUnique();
    }
}
