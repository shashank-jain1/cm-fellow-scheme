using Microsoft.EntityFrameworkCore;
using CmScheme.Training.Core.Data;

namespace CmScheme.Training.Infrastructure.Data;

public sealed class TrainingQueryDbContext(
    DbContextOptions<TrainingQueryDbContext> options) : TrainingDbContext(options), ITrainingQueryDbContext
{
}
