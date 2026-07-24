import { Button } from 'primereact/button';
import { useApproveCertificate, useRejectCertificate } from '../queries';
import { formatDate } from '../../../shared/utils/format';
import type { CertificateApplicationDto } from '../types';

interface CertificateApprovalQueueProps {
  certificates: CertificateApplicationDto[];
  isLoading?: boolean;
}

export default function CertificateApprovalQueue({ certificates, isLoading }: CertificateApprovalQueueProps) {
  const approveMutation = useApproveCertificate();
  const rejectMutation = useRejectCertificate();

  const pending = certificates.filter((c) => c.status === 'pending');

  if (isLoading) {
    return (
      <div style={{ padding: 20 }}>
        {[1, 2, 3].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '25%', height: 14 }} />
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (pending.length === 0) {
    return (
      <div className="empty-state">
        <i className="pi pi-check-circle" />
        <h3>No pending approvals</h3>
        <p>All certificate applications have been reviewed</p>
      </div>
    );
  }

  return (
    <div className="table-wrapper">
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr style={{ background: 'var(--navy-50)' }}>
            {['Applicant', 'Program', 'Duration', 'Applied', 'Actions'].map((h) => (
              <th
                key={h}
                style={{
                  padding: '12px 16px',
                  textAlign: 'left',
                  fontSize: 12,
                  fontWeight: 600,
                  color: 'var(--text-secondary)',
                  textTransform: 'uppercase',
                  letterSpacing: '0.5px',
                  borderBottom: '1px solid var(--border-color)',
                }}
              >
                {h}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {pending.map((c) => (
            <tr key={c.certificateId} style={{ borderBottom: '1px solid var(--border-light)' }}>
              <td style={{ padding: '14px 16px', fontWeight: 500, fontSize: 14 }}>{c.applicantName}</td>
              <td style={{ padding: '14px 16px', fontSize: 13 }}>{c.programName}</td>
              <td style={{ padding: '14px 16px', fontSize: 13 }}>{c.durationDays} days</td>
              <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-muted)' }}>{formatDate(c.startDate)}</td>
              <td style={{ padding: '14px 16px' }}>
                <div style={{ display: 'flex', gap: 6 }}>
                  <Button
                    icon="pi pi-check"
                    className="btn btn-sm btn-primary"
                    onClick={() => approveMutation.mutate(c.certificateId)}
                    disabled={approveMutation.isPending}
                  />
                  <Button
                    icon="pi pi-times"
                    className="btn btn-sm btn-secondary"
                    onClick={() => rejectMutation.mutate(c.certificateId)}
                    disabled={rejectMutation.isPending}
                  />
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
