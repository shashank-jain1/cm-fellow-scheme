using CmScheme.Certificate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CmScheme.Certificate.Core.Data;

public interface ICertificateQueryDbContext
{
    IQueryable<CertificateApplication> CertificateApplications { get; }
    IQueryable<ExitRecord> ExitRecords { get; }
    IQueryable<CertificateVerification> CertificateVerifications { get; }
}
