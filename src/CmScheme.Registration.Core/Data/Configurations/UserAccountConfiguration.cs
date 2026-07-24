using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.HasKey(ua => ua.UserAccountId);

        builder.Property(ua => ua.Username)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ua => ua.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(ua => ua.Role)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne<Applicant>()
            .WithMany()
            .HasForeignKey(ua => ua.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
