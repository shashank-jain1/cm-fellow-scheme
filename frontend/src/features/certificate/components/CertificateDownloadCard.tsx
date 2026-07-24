import { Button } from 'primereact/button';
import { Tag } from 'primereact/tag';
import { downloadCertificate } from '../api';
import type { CertificateApplicationDto } from '../types';

interface CertificateDownloadCardProps {
  certificate: CertificateApplicationDto;
}

export default function CertificateDownloadCard({ certificate }: CertificateDownloadCardProps) {
  const isApproved = certificate.status === 'approved' || certificate.status === 'issued';

  return (
    <div className="kpi-card" style={{ padding: 24, display: 'flex', alignItems: 'center', gap: 20 }}>
      <div
        style={{
          width: 56,
          height: 56,
          borderRadius: 14,
          background: isApproved ? 'var(--emerald-100)' : 'var(--amber-100)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          flexShrink: 0,
        }}
      >
        <i
          className={`pi ${isApproved ? 'pi-verified' : 'pi-hourglass'}`}
          style={{ fontSize: 24, color: isApproved ? 'var(--emerald-500)' : 'var(--amber-500)' }}
        />
      </div>
      <div style={{ flex: 1, minWidth: 0 }}>
        <div style={{ fontSize: 14, fontWeight: 600, color: 'var(--text-primary)' }}>{certificate.applicantName}</div>
        <div style={{ fontSize: 12, color: 'var(--text-secondary)', marginTop: 2 }}>
          {certificate.programName} &middot; {certificate.durationDays} days
        </div>
        <Tag
          value={certificate.status}
          severity={isApproved ? 'success' : 'warning'}
          style={{ marginTop: 6 }}
        />
      </div>
      {isApproved && certificate.certificatePdfPath && (
        <Button
          icon="pi pi-download"
          label="Download"
          className="btn btn-primary"
          onClick={() => downloadCertificate(certificate.certificateId)}
        />
      )}
    </div>
  );
}
