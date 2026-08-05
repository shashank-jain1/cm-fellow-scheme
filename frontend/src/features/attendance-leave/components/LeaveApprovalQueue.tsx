import { useState } from 'react';
import { useLeaveStatus, useApproveLeave } from '../queries';
import { useAuth } from '../../auth';
import LeaveApprovalTable from './LeaveApprovalTable';
import LeaveApprovalActions from './LeaveApprovalActions';

export default function LeaveApprovalQueue() {
  const { user } = useAuth();
  const { data: leaveStatuses, isLoading } = useLeaveStatus(user?.userAccountId ?? 0);
  const approveMutation = useApproveLeave();
  const [remarksMap, setRemarksMap] = useState<Record<number, string>>({});

  const pendingLeaves = (leaveStatuses ?? []).filter(
    (l) => l.status === 'Pending'
  );

  const handleApprove = (leaveApplicationId: number) => {
    approveMutation.mutate({
      leaveApplicationId,
      status: 'Approved',
      remarks: remarksMap[leaveApplicationId] ?? '',
    });
  };

  const handleReject = (leaveApplicationId: number) => {
    approveMutation.mutate({
      leaveApplicationId,
      status: 'Rejected',
      remarks: remarksMap[leaveApplicationId] ?? '',
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

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 16 }}>
      <LeaveApprovalTable leaves={pendingLeaves} />
      {pendingLeaves.map((request) => (
        <LeaveApprovalActions
          key={request.leaveApplicationId}
          leaveApplicationId={request.leaveApplicationId}
          remarks={remarksMap[request.leaveApplicationId] || ''}
          onRemarksChange={(value) =>
            setRemarksMap((prev) => ({ ...prev, [request.leaveApplicationId]: value }))
          }
          onApprove={handleApprove}
          onReject={handleReject}
          isPending={approveMutation.isPending}
        />
      ))}
    </div>
  );
}
