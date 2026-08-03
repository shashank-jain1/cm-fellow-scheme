import { useCertificates, useGenerateCompletionCertificate, useGenerateExperienceLetter } from '../queries';
import CertificateApprovalQueue from '../components/CertificateApprovalQueue';
import CertificateDownloadCard from '../components/CertificateDownloadCard';
import { SkeletonTable, AppButton } from '../../../shared/components/ui';
import { ToastService } from '../../../shared/utils/toast';

export default function CertificateApprovalPage() {
  const { data: certificates, isLoading } = useCertificates();
  const genCompletion = useGenerateCompletionCertificate();
  const genExperience = useGenerateExperienceLetter();

  const approved = (certificates ?? []).filter(
    (c) => c.status === 'Approved' || c.status === 'Issued'
  );

  const handleGenerateCompletion = async (applicantId: number) => {
    try {
      await genCompletion.mutateAsync(applicantId);
      ToastService.success('Completion certificate generated!');
    } catch {
      ToastService.error('Failed to generate completion certificate');
    }
  };

  const handleGenerateExperience = async (applicantId: number) => {
    try {
      await genExperience.mutateAsync(applicantId);
      ToastService.success('Experience letter generated!');
    } catch {
      ToastService.error('Failed to generate experience letter');
    }
  };

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

      {approved.length > 0 && (
        <div className="card" style={{ padding: 24, marginTop: 32 }}>
          <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Generate Additional Documents</h3>
          <div style={{ display: 'flex', flexDirection: 'column', gap: 12, maxWidth: 300 }}>
            {approved.map((c) => (
              <div key={c.certificateId} style={{ display: 'flex', gap: 8 }}>
                <AppButton
                  variant="accent"
                  size="sm"
                  icon="pi pi-file-pdf"
                  onClick={() => handleGenerateCompletion(c.applicantId)}
                  loading={genCompletion.isPending}
                >
                  Completion Cert
                </AppButton>
                <AppButton
                  variant="accent"
                  size="sm"
                  icon="pi pi-file"
                  onClick={() => handleGenerateExperience(c.applicantId)}
                  loading={genExperience.isPending}
                >
                  Experience Letter
                </AppButton>
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
}
