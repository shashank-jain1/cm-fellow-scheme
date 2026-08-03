using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using CmScheme.Masters.Core.Entities;

namespace CmScheme.Masters.Core.Data;

public interface IMastersQueryDbContext
{
    DatabaseFacade Database { get; }
    DbSet<State> States { get; }
    DbSet<Division> Divisions { get; }
    DbSet<District> Districts { get; }
    DbSet<Block> Blocks { get; }
    DbSet<GramPanchayat> GramPanchayats { get; }
    DbSet<Project> Projects { get; }
    DbSet<Work> Works { get; }
    DbSet<LookupMaster> LookupMasters { get; }
    DbSet<Department> Departments { get; }
}
