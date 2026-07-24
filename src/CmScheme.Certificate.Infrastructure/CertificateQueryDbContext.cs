using CmScheme.Certificate.Core.Data;
using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Infrastructure;

public class CertificateQueryDbContext : ICertificateQueryDbContext
{
    private readonly CertificateDbContext _dbContext;

    public CertificateQueryDbContext(CertificateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<CertificateApplication> CertificateApplications => _dbContext.CertificateApplications;
    public IQueryable<ExitRecord> ExitRecords => _dbContext.ExitRecords;
}
