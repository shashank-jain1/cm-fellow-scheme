using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Data;
using CmScheme.Training.Core.Data.Configurations;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Infrastructure.Data;

public abstract class TrainingDbContext(DbContextOptions options)
    : BaseDbContext(options)
{
    public DbSet<TrainingSchedule> TrainingSchedules => Set<TrainingSchedule>();
    public DbSet<TrainingParticipant> TrainingParticipants => Set<TrainingParticipant>();
    public DbSet<MeetingParticipant> MeetingParticipants => Set<MeetingParticipant>();
    public DbSet<TrainingMaterial> TrainingMaterials => Set<TrainingMaterial>();
    public DbSet<TrainingMaterialUpload> TrainingMaterialUploads => Set<TrainingMaterialUpload>();
    public DbSet<TrainingCompletion> TrainingCompletions => Set<TrainingCompletion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TrainingCompletionConfiguration());
    }
}
