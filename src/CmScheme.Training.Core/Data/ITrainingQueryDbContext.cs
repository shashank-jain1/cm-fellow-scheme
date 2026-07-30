using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using CmScheme.Training.Core.Entities;

namespace CmScheme.Training.Core.Data;

public interface ITrainingQueryDbContext
{
    DatabaseFacade Database { get; }
    DbSet<TrainingSchedule> TrainingSchedules { get; }
    DbSet<TrainingParticipant> TrainingParticipants { get; }
    DbSet<TrainingMaterial> TrainingMaterials { get; }
    DbSet<TrainingCompletion> TrainingCompletions { get; }
}
