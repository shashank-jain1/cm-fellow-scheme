using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class LeaveBalanceConfiguration : IEntityTypeConfiguration<LeaveBalance>
{
    public void Configure(EntityTypeBuilder<LeaveBalance> builder)
    {
        builder.ToTable("LeaveBalance");
        builder.HasKey(lb => lb.LeaveBalanceId);

        builder.Property(lb => lb.TotalDays)
            .HasColumnType("decimal(5,2)");

        builder.Property(lb => lb.UsedDays)
            .HasColumnType("decimal(5,2)");

        builder.Property(lb => lb.RemainingDays)
            .HasColumnType("decimal(5,2)");

        builder.HasOne(lb => lb.UserAccount)
            .WithMany()
            .HasForeignKey(lb => lb.UserAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(lb => lb.LeaveType)
            .WithMany()
            .HasForeignKey(lb => lb.LeaveTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(lb => new { lb.UserAccountId, lb.LeaveTypeId, lb.Year })
            .IsUnique();
    }
}
