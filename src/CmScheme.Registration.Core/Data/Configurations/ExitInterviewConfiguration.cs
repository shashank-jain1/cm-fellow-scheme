using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Registration.Core.Entities;

namespace CmScheme.Registration.Core.Data.Configurations;

public sealed class ExitInterviewConfiguration : IEntityTypeConfiguration<ExitInterview>
{
    public void Configure(EntityTypeBuilder<ExitInterview> builder)
    {
        builder.ToTable("ExitInterviews");
        builder.HasKey(e => e.ExitInterviewId);
        builder.Property(e => e.ImprovementSuggestions).HasMaxLength(1000);
        builder.Property(e => e.WhatWorkedWell).HasMaxLength(1000);
    }
}
