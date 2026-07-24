import { useState } from 'react';
import { Button } from 'primereact/button';
import { InputTextarea } from 'primereact/inputtextarea';
import { useLeaveApprovalQueue, useApproveLeave, useRejectLeave } from '../queries';
import AttendanceStatusBadge from './AttendanceStatusBadge';

export default function LeaveApprovalQueue() {
  const { data: queue, isLoading } = useLeaveApprovalQueue();
  const approveMutation = useApproveLeave();
  const rejectMutation = useRejectLeave();
  const [remarksMap, setRemarksMap] = useState<Record<string, string>>({});

  const handleApprove = (applicationNo: string) => {
    approveMutation.mutate({ applicationNo, remarks: remarksMap[applicationNo] });
  };

  const handleReject = (applicationNo: string) => {
    rejectMutation.mutate({ applicationNo, remarks: remarksMap[applicationNo] });
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

  if (!queue || queue.length === 0) {
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
      {queue.map((request) => (
        <div
          key={request.leaveApplicationNo}
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
                {request.employeeName}
              </div>
              <div style={{ fontSize: 13, color: 'var(--text-secondary)', marginTop: 4 }}>
                {request.leaveType} — {request.leavePeriod} ({request.numberOfDays} day{request.numberOfDays > 1 ? 's' : ''})
              </div>
            </div>
            <AttendanceStatusBadge status={request.approvalStatus} />
          </div>

          <InputTextarea
            value={remarksMap[request.leaveApplicationNo] || ''}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) =>
              setRemarksMap((prev) => ({ ...prev, [request.leaveApplicationNo]: e.target.value }))
            }
            placeholder="Add remarks (optional)"
            rows={2}
            style={{ width: '100%', marginBottom: 12 }}
          />

          <div style={{ display: 'flex', gap: 12 }}>
            <Button
              label="Approve"
              icon="pi pi-check"
              className="btn btn-primary"
              loading={approveMutation.isPending}
              onClick={() => handleApprove(request.leaveApplicationNo)}
            />
            <Button
              label="Reject"
              icon="pi pi-times"
              className="btn btn-danger"
              loading={rejectMutation.isPending}
              onClick={() => handleReject(request.leaveApplicationNo)}
            />
          </div>
        </div>
      ))}
    </div>
  );
}
