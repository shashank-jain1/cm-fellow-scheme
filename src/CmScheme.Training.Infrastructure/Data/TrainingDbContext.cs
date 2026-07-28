using Microsoft.EntityFrameworkCore;
using CmScheme.Common.Core.Data;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Infrastructure.Data;

public abstract class TrainingDbContext(DbContextOptions options)
    : BaseDbContext(options)
{
    public DbSet<TrainingSchedule> TrainingSchedules => Set<TrainingSchedule>();
    public DbSet<TrainingParticipant> TrainingParticipants => Set<TrainingParticipant>();
    public DbSet<MeetingParticipant> MeetingParticipants => Set<MeetingParticipant>();
}
