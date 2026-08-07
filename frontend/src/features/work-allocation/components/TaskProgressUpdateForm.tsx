import { useState } from 'react';
import { AppSelect, AppTextarea, AppSlider } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { ToastService } from '../../../shared/utils/toast';
import { useUpdateProgress } from '../queries';

interface TaskProgressUpdateFormProps {
  workAllocationId: number;
  currentProgress?: number;
  currentStatus?: string;
  onSuccess?: () => void;
}

const STATUS_OPTIONS = [
  { label: 'In Progress', value: 'In Progress' },
  { label: 'Completed', value: 'Completed' },
  { label: 'Blocked', value: 'Blocked' },
];

export default function TaskProgressUpdateForm({
  workAllocationId,
  currentProgress = 0,
  currentStatus = 'In Progress',
  onSuccess,
}: TaskProgressUpdateFormProps) {
  const updateMutation = useUpdateProgress();
  const [progress, setProgress] = useState(currentProgress);
  const [status, setStatus] = useState<string | number>(currentStatus);
  const [comments, setComments] = useState('');

  const handleSubmit = async () => {
    try {
      await updateMutation.mutateAsync({
        workAllocationId,
        data: {
          progressPercentage: progress,
          status: String(status),
          progressNotes: comments.trim() || undefined,
        },
      });
      ToastService.success('Progress updated successfully');
      onSuccess?.();
    } catch {
      ToastService.error('Failed to update progress');
    }
  };

  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>
        Update Task Progress
      </h3>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 16, maxWidth: 480 }}>
        <AppSlider value={progress} onChange={setProgress} />
        <div className="form-field">
          <label>Status</label>
          <AppSelect
            value={status}
            onChange={(val: string | number) => setStatus(val)}
            options={STATUS_OPTIONS}
          />
        </div>
        <div className="form-field">
          <label>Comments</label>
          <AppTextarea
            value={comments}
            onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setComments(e.target.value)}
            placeholder="Add progress notes..."
            rows={3}
          />
        </div>
        <div style={{ display: 'flex', gap: 12, justifyContent: 'flex-end' }}>
          <AppButton
            variant="primary"
            onClick={handleSubmit}
            loading={updateMutation.isPending}
            disabled={updateMutation.isPending}
          >
            Save Progress
          </AppButton>
        </div>
      </div>
    </div>
  );
}
