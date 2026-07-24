import { useLeaveStatus } from '../queries';
import AttendanceStatusBadge from '../components/AttendanceStatusBadge';

export default function LeaveStatusPage() {
  const { data: leaveRecords, isLoading } = useLeaveStatus();

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Leave Status</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Track your leave application status
          </p>
        </div>
      </div>

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
              <tr style={{ background: 'var(--navy-50)' }}>
                {['Application No', 'Leave Type', 'Period', 'Days', 'Status', 'Approved By', 'Date'].map((h) => (
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
              {leaveRecords.map((record) => (
                <tr key={record.leaveApplicationNo} style={{ borderBottom: '1px solid var(--border-light)' }}>
                  <td style={{ padding: '14px 16px', fontSize: 13, fontWeight: 500 }}>
                    {record.leaveApplicationNo}
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 13 }}>{record.leaveType}</td>
                  <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                    {record.leavePeriod}
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 13 }}>{record.numberOfDays}</td>
                  <td style={{ padding: '14px 16px' }}>
                    <AttendanceStatusBadge status={record.approvalStatus} />
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                    {record.approvedBy || '—'}
                  </td>
                  <td style={{ padding: '14px 16px', fontSize: 13, color: 'var(--text-muted)' }}>
                    {record.approvalDate || '—'}
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
