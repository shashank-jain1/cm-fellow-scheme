import { useCertificates } from '../queries';
import CertificateApprovalQueue from '../components/CertificateApprovalQueue';
import CertificateDownloadCard from '../components/CertificateDownloadCard';
import { SkeletonTable } from '../../../shared/components/ui';

export default function CertificateApprovalPage() {
  const { data: certificates, isLoading } = useCertificates();

  const approved = (certificates ?? []).filter(
    (c) => c.status === 'Approved' || c.status === 'Issued'
  );

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Certificate Management</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Review and approve certificate applications
          </p>
        </div>
      </div>

      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Pending Approvals</h3>
      {isLoading ? (
        <SkeletonTable columns={5} />
      ) : (
        <CertificateApprovalQueue certificates={certificates ?? []} isLoading={isLoading} />
      )}

      {approved.length > 0 && (
        <>
          <h3 style={{ fontSize: 16, fontWeight: 600, marginTop: 32, marginBottom: 16 }}>Approved Certificates</h3>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(400px, 1fr))', gap: 16 }}>
            {approved.map((c) => (
              <CertificateDownloadCard key={c.certificateId} certificate={c} />
            ))}
          </div>
        </>
      )}
    </div>
  );
}
