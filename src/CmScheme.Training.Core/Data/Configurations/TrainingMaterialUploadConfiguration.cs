using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Core.Data.Configurations;

public sealed class TrainingMaterialUploadConfiguration : IEntityTypeConfiguration<TrainingMaterialUpload>
{
    public void Configure(EntityTypeBuilder<TrainingMaterialUpload> builder)
    {
        builder.ToTable("TrainingMaterialUploads");
        builder.HasKey(x => x.TrainingMaterialUploadId);
        builder.Property(x => x.FileName).HasMaxLength(200).IsRequired();
        builder.Property(x => x.FilePath).HasMaxLength(500).IsRequired();
    }
}
