using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Core.Data.Configurations;

public sealed class TrainingMaterialConfiguration : IEntityTypeConfiguration<TrainingMaterial>
{
    public void Configure(EntityTypeBuilder<TrainingMaterial> builder)
    {
        builder.ToTable("TrainingMaterials");
        builder.HasKey(x => x.TrainingMaterialId);
        builder.Property(x => x.MaterialName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.FilePath).HasMaxLength(500).IsRequired();
        builder.Property(x => x.ContentType).HasMaxLength(100);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
    }
}
