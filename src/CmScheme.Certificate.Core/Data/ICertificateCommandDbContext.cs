using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Core.Data;

public interface ICertificateCommandDbContext
{
    DbSet<CertificateApplication> CertificateApplications { get; }
    DbSet<ExitRecord> ExitRecords { get; }
    DbSet<CertificateVerification> CertificateVerifications { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
