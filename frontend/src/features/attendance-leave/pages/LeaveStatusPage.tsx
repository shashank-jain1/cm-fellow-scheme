import { useLeaveStatus } from '../queries';
import { useAuth } from '../../auth';
import PageHeader from '../../../shared/components/ui/PageHeader';
import StatusTag from '../../../shared/components/ui/StatusTag';

export default function LeaveStatusPage() {
  const { user } = useAuth();
  const { data: leaveRecords, isLoading } = useLeaveStatus(user?.userAccountId ?? 0);

  return (
    <div>
      <PageHeader
        title="Leave Status"
        subtitle="Track your leave application status"
      />

      <div className="table-wrapper">
        {isLoading ? (
          <div style={{ padding: 20 }}>
            {[1, 2, 3].map((n) => (
              <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
                <div className="skeleton" style={{ width: '15%', height: 14 }} />
                <div className="skeleton" style={{ width: '12%', height: 14 }} />
                <div className="skeleton" style={{ width: '10%', height: 14 }} />
                <div className="skeleton" style={{ width: '10%', height: 14 }} />
              </div>
            ))}
          </div>
        ) : leaveRecords && leaveRecords.length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ background: 'var(--bg-primary)' }}>
                {['Leave Type', 'From', 'To', 'Days', 'Status'].map((h) => (
                  <th
                    key={h}
                    style={{
                      padding: '12px 16px',
                      textAlign: 'left',
                      fontSize: 12,
                      fontWeight: 700,
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
              {leaveRecords.map((record) => (
                <tr key={record.leaveApplicationId} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ padding: '14px 16px', fontSize: 13, fontWeight: 600, color: 'var(--text-primary)' }}>{record.leaveType}</td>
                  <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                    {record.fromDate}
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                    {record.toDate}
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 13, fontWeight: 600 }}>{record.numberOfDays}</td>
                  <td style={{ padding: '14px 16px' }}>
                    <StatusTag value={record.status} />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <div className="empty-state">
            <i className="pi pi-inbox" />
            <h3>No leave records</h3>
            <p>You haven't applied for any leaves yet.</p>
          </div>
        )}
      </div>
    </div>
  );
}
