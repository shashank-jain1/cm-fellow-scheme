using Microsoft.EntityFrameworkCore;
using CmScheme.Training.Core.Data;

namespace CmScheme.Training.Infrastructure.Data;

public sealed class TrainingCommandDbContext(
    DbContextOptions<TrainingCommandDbContext> options) : TrainingDbContext(options), ITrainingCommandDbContext
{
}
