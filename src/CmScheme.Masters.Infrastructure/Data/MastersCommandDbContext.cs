using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Infrastructure.Data;

public sealed class MastersCommandDbContext : MastersDbContext, IMastersCommandDbContext
{
    public MastersCommandDbContext(DbContextOptions<MastersCommandDbContext> options)
        : base(options)
    {
    }

    DbSet<State> IMastersCommandDbContext.States => States;
    DbSet<Division> IMastersCommandDbContext.Divisions => Divisions;
    DbSet<District> IMastersCommandDbContext.Districts => Districts;
    DbSet<Block> IMastersCommandDbContext.Blocks => Blocks;
    DbSet<GramPanchayat> IMastersCommandDbContext.GramPanchayats => GramPanchayats;
    DbSet<Project> IMastersCommandDbContext.Projects => Projects;
    DbSet<Work> IMastersCommandDbContext.Works => Works;
    DbSet<LookupMaster> IMastersCommandDbContext.LookupMasters => LookupMasters;

    int IMastersCommandDbContext.SaveChanges()
    {
        return base.SaveChanges();
    }

    async Task<int> IMastersCommandDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
