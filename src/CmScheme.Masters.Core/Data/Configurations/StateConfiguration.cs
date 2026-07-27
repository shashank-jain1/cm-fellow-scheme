using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data.Configurations;

public sealed class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("State");
        builder.HasKey(s => s.StateId);

        builder.Property(s => s.StateName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.StateCode)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(s => s.StateShortName)
            .HasMaxLength(20);

        builder.Property(s => s.DisplayOrder);

        builder.Property(s => s.IsActive)
            .HasDefaultValue(true);
    }
}
