import { useState } from 'react';
import { AppTextarea } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { ToastService } from '../../../shared/utils/toast';
import { useVerifyTask } from '../queries';
import type { VerifyTaskCommand } from '../types';

interface TaskVerificationPanelProps {
  workAllocationId: number;
  onSuccess?: () => void;
}

export default function TaskVerificationPanel({
  workAllocationId,
  onSuccess,
}: TaskVerificationPanelProps) {
  const verifyMutation = useVerifyTask();
  const [comments, setComments] = useState('');

  const handleVerify = async (status: VerifyTaskCommand['status']) => {
    try {
      await verifyMutation.mutateAsync({
        workAllocationId,
        command: { status, comments: comments.trim() },
      });
      ToastService.success(`Task ${status.toLowerCase()} successfully`);
      setComments('');
      onSuccess?.();
    } catch {
      ToastService.error(`Failed to ${status.toLowerCase()} task`);
    }
  };

  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>
        Task Verification
      </h3>
      <div className="form-group" style={{ marginBottom: 16 }}>
        <label className="form-label">Verification Comments</label>
        <AppTextarea
          value={comments}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) =>
            setComments(e.target.value)
          }
          placeholder="Add verification comments..."
          rows={3}
        />
      </div>
      <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end' }}>
        <AppButton
          variant="danger"
          onClick={() => handleVerify('Rejected')}
          loading={verifyMutation.isPending}
          disabled={verifyMutation.isPending}
        >
          Reject
        </AppButton>
        <AppButton
          variant="primary"
          onClick={() => handleVerify('Approved')}
          loading={verifyMutation.isPending}
          disabled={verifyMutation.isPending}
        >
          Approve
        </AppButton>
      </div>
    </div>
  );
}
