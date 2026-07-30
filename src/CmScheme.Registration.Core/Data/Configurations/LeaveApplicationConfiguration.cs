using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class LeaveApplicationConfiguration : IEntityTypeConfiguration<LeaveApplication>
{
    public void Configure(EntityTypeBuilder<LeaveApplication> builder)
    {
        builder.ToTable("LeaveApplication");
        builder.HasKey(la => la.LeaveApplicationId);

        builder.Property(la => la.ApplicationNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(la => la.ApplicationNumber)
            .IsUnique();

        builder.Property(la => la.NumberOfDays)
            .HasColumnType("decimal(5,2)");

        builder.Property(la => la.Reason)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(la => la.AttachmentPath)
            .HasMaxLength(255);

        builder.Property(la => la.Status)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(la => la.ApprovalRemarks)
            .HasMaxLength(500);

        builder.HasOne(la => la.UserAccount)
            .WithMany()
            .HasForeignKey(la => la.UserAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(la => la.LeaveType)
            .WithMany()
            .HasForeignKey(la => la.LeaveTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(la => la.ApprovedByUser)
            .WithMany()
            .HasForeignKey(la => la.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
