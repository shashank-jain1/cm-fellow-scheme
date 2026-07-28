import { Tag } from 'primereact/tag';
import { useGenerateCertificate } from '../queries';
import { getDownloadUrl } from '../api';
import { useAuth } from '../../../features/auth';
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
  const { user } = useAuth();

  const handleGenerate = () => {
    generateMutation.mutate(certificate.certificateId);
  };

  const handleDownload = () => {
    const url = getDownloadUrl(certificate.certificateId);
    window.open(url, '_blank');
  };

  return (
    <div className="kpi-card" style={{ padding: 24, display: 'flex', alignItems: 'center', gap: 20 }}>
      <div
        style={{
          width: 56,
          height: 56,
          borderRadius: 14,
          background: isIssued ? 'var(--emerald-100)' : 'var(--amber-100)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          flexShrink: 0,
        }}
      >
        <i
          className={`pi ${isIssued ? 'pi-verified' : 'pi-hourglass'}`}
          style={{ fontSize: 24, color: isIssued ? 'var(--emerald-500)' : 'var(--amber-500)' }}
        />
      </div>
      <div style={{ flex: 1, minWidth: 0 }}>
        <div style={{ fontSize: 14, fontWeight: 600, color: 'var(--text-primary)' }}>{certificate.applicantName}</div>
        <div style={{ fontSize: 12, color: 'var(--text-secondary)', marginTop: 2 }}>
          {certificate.programName} &middot; {certificate.durationDays} days
        </div>
        <Tag
          value={certificate.status}
          severity={isIssued ? 'success' : 'warning'}
          style={{ marginTop: 6 }}
        />
      </div>
      <div style={{ display: 'flex', gap: 8 }}>
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
