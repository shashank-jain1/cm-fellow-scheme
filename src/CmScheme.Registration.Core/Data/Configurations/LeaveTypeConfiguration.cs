using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable("LeaveType");
        builder.HasKey(lt => lt.LeaveTypeId);

        builder.Property(lt => lt.TypeName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(lt => lt.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(lt => lt.Code)
            .IsUnique();
    }
}
