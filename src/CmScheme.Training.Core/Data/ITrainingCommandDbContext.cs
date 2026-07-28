using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Core.Data;

public interface ITrainingCommandDbContext
{
    DatabaseFacade Database { get; }
    DbSet<TrainingSchedule> TrainingSchedules { get; }
    DbSet<TrainingParticipant> TrainingParticipants { get; }
    DbSet<MeetingParticipant> MeetingParticipants { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
