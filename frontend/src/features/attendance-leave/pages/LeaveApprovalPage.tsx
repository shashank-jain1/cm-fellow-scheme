import LeaveApprovalQueue from '../components/LeaveApprovalQueue';

export default function LeaveApprovalPage() {
  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Leave Approvals</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>
            Review and approve pending leave requests
          </p>
        </div>
      </div>

      <LeaveApprovalQueue />
    </div>
  );
}
