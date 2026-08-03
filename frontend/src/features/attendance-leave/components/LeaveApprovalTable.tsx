import StatusTag from '../../../shared/components/ui/StatusTag';
import type { LeaveStatusDto } from '../../attendance-leave/types';

interface LeaveApprovalTableProps {
  leaves: LeaveStatusDto[];
}

export default function LeaveApprovalTable({ leaves }: LeaveApprovalTableProps) {
  if (leaves.length === 0) {
    return (
      <div className="empty-state">
        <i className="pi pi-inbox" />
        <h3>No pending requests</h3>
        <p>There are no leave requests awaiting approval.</p>
      </div>
    );
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
      {leaves.map((request) => (
        <div
          key={request.leaveApplicationId}
          style={{
            padding: 20,
            border: '1px solid var(--border-color)',
            borderRadius: 12,
            background: 'var(--surface-card)',
          }}
        >
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: 12 }}>
            <div>
              <div style={{ fontSize: 15, fontWeight: 600, color: 'var(--text-primary)' }}>
                {request.applicationNumber}
              </div>
              <div style={{ fontSize: 13, color: 'var(--text-secondary)', marginTop: 4 }}>
                {request.leaveTypeName} — {request.fromDate} to {request.toDate} ({request.numberOfDays} day{request.numberOfDays > 1 ? 's' : ''})
              </div>
              {request.reason && (
                <div style={{ fontSize: 13, color: 'var(--text-muted)', marginTop: 4 }}>
                  Reason: {request.reason}
                </div>
              )}
            </div>
            <StatusTag value={request.status} />
          </div>
        </div>
      ))}
    </div>
  );
}
