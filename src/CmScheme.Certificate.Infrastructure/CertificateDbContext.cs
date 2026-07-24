using CmScheme.Common.Core.Data;
using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Data.Configurations;
using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Infrastructure;

public class CertificateDbContext : BaseDbContext, ICertificateCommandDbContext, ICertificateQueryDbContext
{
    public DbSet<CertificateApplication> CertificateApplications => Set<CertificateApplication>();
    public DbSet<ExitRecord> ExitRecords => Set<ExitRecord>();

    public CertificateDbContext(DbContextOptions<CertificateDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CertificateApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new ExitRecordConfiguration());
    }

    IQueryable<CertificateApplication> ICertificateQueryDbContext.CertificateApplications => CertificateApplications;
    IQueryable<ExitRecord> ICertificateQueryDbContext.ExitRecords => ExitRecords;
}
