import { Tag } from 'primereact/tag';
import { useGenerateCertificate } from '../queries';
import { getDownloadUrl } from '../api';
import { AppButton } from '../../../shared/components/ui';
import type { CertificateApplicationDto } from '../types';

interface CertificateDownloadCardProps {
  certificate: CertificateApplicationDto;
}

export default function CertificateDownloadCard({ certificate }: CertificateDownloadCardProps) {
  const isApproved = certificate.status === 'Approved';
  const isIssued = certificate.status === 'Issued';
  const canGenerate = isApproved && !isIssued;
  const canDownload = isIssued && certificate.certificatePdfPath;

  const generateMutation = useGenerateCertificate();

  const handleGenerate = () => {
    generateMutation.mutate(certificate.certificateId);
  };

  const handleDownload = () => {
    const url = getDownloadUrl(certificate.certificateId);
    window.open(url, '_blank');
  };

  return (
    <div className="card" style={{ padding: 'var(--space-4) var(--space-5)', display: 'flex', alignItems: 'center', gap: 'var(--space-4)' }}>
      <div
        style={{
          width: 44,
          height: 44,
          borderRadius: 'var(--radius-md)',
          background: isIssued ? 'var(--filing-100)' : 'var(--ledger-100)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          flexShrink: 0,
        }}
      >
        <i
          className={`pi ${isIssued ? 'pi-verified' : 'pi-hourglass'}`}
          style={{ fontSize: 20, color: isIssued ? 'var(--filing)' : 'var(--ledger)' }}
        />
      </div>
      <div style={{ flex: 1, minWidth: 0 }}>
        <div style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-base)', fontWeight: 600, color: 'var(--text-heading)' }}>{certificate.applicantName}</div>
        <div style={{ fontFamily: 'var(--font-body)', fontSize: 'var(--text-xs)', color: 'var(--text-muted)', marginTop: 1 }}>
          {certificate.programName} &middot; {certificate.durationDays} days
        </div>
        <Tag
          value={certificate.status}
          severity={isIssued ? 'success' : 'warning'}
          style={{ marginTop: 4 }}
        />
      </div>
      <div style={{ display: 'flex', gap: 'var(--space-2)' }}>
        {canGenerate && (
          <AppButton
            variant="primary"
            size="sm"
            icon="pi pi-file-pdf"
            onClick={handleGenerate}
            loading={generateMutation.isPending}
          >
            Generate
          </AppButton>
        )}
        {canDownload && (
          <AppButton
            variant="primary"
            size="sm"
            icon="pi pi-download"
            onClick={handleDownload}
          >
            Download
          </AppButton>
        )}
      </div>
    </div>
  );
}
