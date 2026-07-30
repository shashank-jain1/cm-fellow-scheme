import { useState } from 'react';
import { AppTextarea } from '../../../shared/components/forms';
import AppButton from '../../../shared/components/ui/AppButton';
import { useLeaveStatus, useApproveLeave } from '../queries';
import StatusTag from '../../../shared/components/ui/StatusTag';
import { useAuth } from '../../auth';

export default function LeaveApprovalQueue() {
  const { user } = useAuth();
  const { data: leaveStatuses, isLoading } = useLeaveStatus(user?.userAccountId ?? 0);
  const approveMutation = useApproveLeave();
  const [remarksMap, setRemarksMap] = useState<Record<number, string>>({});

  const pendingLeaves = (leaveStatuses ?? []).filter(
    (l) => l.status === 'Pending'
  );

  const handleApprove = (leaveApplicationId: number) => {
    if (!user) return;
    approveMutation.mutate({
      leaveApplicationId,
      approvedBy: user.userAccountId,
      action: 'Approved',
      remarks: remarksMap[leaveApplicationId],
    });
  };

  const handleReject = (leaveApplicationId: number) => {
    if (!user) return;
    approveMutation.mutate({
      leaveApplicationId,
      approvedBy: user.userAccountId,
      action: 'Rejected',
      remarks: remarksMap[leaveApplicationId],
    });
  };

  if (isLoading) {
    return (
      <div style={{ padding: 20 }}>
        {[1, 2, 3].map((n) => (
          <div key={n} style={{ display: 'flex', gap: 16, padding: '14px 0', borderBottom: '1px solid var(--border-light)' }}>
            <div className="skeleton" style={{ width: '15%', height: 14 }} />
            <div className="skeleton" style={{ width: '20%', height: 14 }} />
            <div className="skeleton" style={{ width: '10%', height: 14 }} />
          </div>
        ))}
      </div>
    );
  }

  if (pendingLeaves.length === 0) {
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
      {pendingLeaves.map((request) => (
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

          <AppTextarea
            value={remarksMap[request.leaveApplicationId] || ''}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) =>
              setRemarksMap((prev) => ({ ...prev, [request.leaveApplicationId]: e.target.value }))
            }
            placeholder="Add remarks (optional)"
            rows={2}
            style={{ width: '100%', marginBottom: 12 }}
          />

          <div style={{ display: 'flex', gap: 12 }}>
            <AppButton
              loading={approveMutation.isPending}
              onClick={() => handleApprove(request.leaveApplicationId)}
              icon="pi pi-check"
            >
              Approve
            </AppButton>
            <AppButton
              variant="danger"
              loading={approveMutation.isPending}
              onClick={() => handleReject(request.leaveApplicationId)}
              icon="pi pi-times"
            >
              Reject
            </AppButton>
          </div>
        </div>
      ))}
    </div>
  );
}
