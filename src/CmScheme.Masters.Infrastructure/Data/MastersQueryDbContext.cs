using Microsoft.EntityFrameworkCore;
using CmScheme.Masters.Core.Data;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Infrastructure.Data;

public sealed class MastersQueryDbContext : MastersDbContext, IMastersQueryDbContext
{
    public MastersQueryDbContext(DbContextOptions<MastersQueryDbContext> options)
        : base(options)
    {
    }

    DbSet<State> IMastersQueryDbContext.States => States;
    DbSet<Division> IMastersQueryDbContext.Divisions => Divisions;
    DbSet<District> IMastersQueryDbContext.Districts => Districts;
    DbSet<Block> IMastersQueryDbContext.Blocks => Blocks;
    DbSet<GramPanchayat> IMastersQueryDbContext.GramPanchayats => GramPanchayats;
    DbSet<Project> IMastersQueryDbContext.Projects => Projects;
    DbSet<Work> IMastersQueryDbContext.Works => Works;
    DbSet<LookupMaster> IMastersQueryDbContext.LookupMasters => LookupMasters;
    DbSet<Department> IMastersQueryDbContext.Departments => Departments;
}
