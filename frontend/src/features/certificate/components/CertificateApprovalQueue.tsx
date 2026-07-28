import { useApproveCertificate, useRejectCertificate } from '../queries';
import { formatDate } from '../../../shared/utils/format';
import { useAuth } from '../../../features/auth';
import { AppButton, EmptyState } from '../../../shared/components/ui';
import type { CertificateApplicationDto } from '../types';
import { SkeletonTable } from '../../../shared/components/ui';

interface CertificateApprovalQueueProps {
  certificates: CertificateApplicationDto[];
  isLoading?: boolean;
}

export default function CertificateApprovalQueue({ certificates, isLoading }: CertificateApprovalQueueProps) {
  const approveMutation = useApproveCertificate();
  const rejectMutation = useRejectCertificate();
  const { user } = useAuth();

  const pending = certificates.filter((c) => c.status === 'Applied');

  const handleApprove = (id: number) => {
    approveMutation.mutate({ id, verifiedBy: user?.username ?? 'admin' });
  };

  const handleReject = (id: number) => {
    rejectMutation.mutate({ id, verifiedBy: user?.username ?? 'admin' });
  };

  if (isLoading) {
    return <SkeletonTable columns={5} />;
  }

  if (pending.length === 0) {
    return <EmptyState icon="pi pi-check-circle" title="No pending approvals" description="All certificate applications have been reviewed" />;
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
                  <AppButton
                    variant="primary"
                    size="sm"
                    icon="pi pi-check"
                    onClick={() => handleApprove(c.certificateId)}
                    loading={approveMutation.isPending}
                  />
                  <AppButton
                    variant="secondary"
                    size="sm"
                    icon="pi pi-times"
                    onClick={() => handleReject(c.certificateId)}
                    loading={rejectMutation.isPending}
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
