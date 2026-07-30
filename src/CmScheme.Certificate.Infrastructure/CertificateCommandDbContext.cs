using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Infrastructure;

public class CertificateCommandDbContext : ICertificateCommandDbContext
{
    private readonly CertificateDbContext _dbContext;

    public CertificateCommandDbContext(CertificateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<CertificateApplication> CertificateApplications => _dbContext.CertificateApplications;
    public DbSet<ExitRecord> ExitRecords => _dbContext.ExitRecords;
    public DbSet<CertificateVerification> CertificateVerifications => _dbContext.CertificateVerifications;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
