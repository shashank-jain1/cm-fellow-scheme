import { useState } from 'react';
import { AppInput } from '../../../shared/components/forms';
import { AppButton, PageHeader, StatusTag } from '../../../shared/components/ui';
import { ToastService } from '../../../shared/utils/toast';
import {
  useActiveReviewCycle,
  useCreateReviewCycle,
  useCloseReviewCycle,
} from '../queries';

export default function ReviewCyclePage() {
  const { data: activeCycle, isLoading } = useActiveReviewCycle();
  const createMutation = useCreateReviewCycle();
  const closeMutation = useCloseReviewCycle();

  const [cycleName, setCycleName] = useState('');
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');

  const handleCreate = async () => {
    if (!cycleName.trim() || !startDate || !endDate) return;
    try {
      await createMutation.mutateAsync({
        cycleName: cycleName.trim(),
        startDate,
        endDate,
      });
      ToastService.success('Review cycle created');
      setCycleName('');
      setStartDate('');
      setEndDate('');
    } catch {
      ToastService.error('Failed to create review cycle');
    }
  };

  const handleClose = async (id: number) => {
    if (!window.confirm('Close this review cycle?')) return;
    try {
      await closeMutation.mutateAsync(id);
      ToastService.success('Review cycle closed');
    } catch {
      ToastService.error('Failed to close review cycle');
    }
  };

  return (
    <div>
      <PageHeader
        title="Review Cycle"
        subtitle="Manage performance review cycles"
      />

      <div className="card" style={{ padding: 24, marginBottom: 24 }}>
        <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>
          Active Cycle
        </h3>
        {isLoading ? (
          <div className="skeleton" style={{ width: 200, height: 20 }} />
        ) : activeCycle ? (
          <div style={{ display: 'flex', alignItems: 'center', gap: 16 }}>
            <div>
              <strong>{activeCycle.cycleName}</strong>
              <span style={{ marginLeft: 12, color: 'var(--text-secondary)' }}>
                {activeCycle.startDate} to {activeCycle.endDate}
              </span>
            </div>
            <StatusTag value={activeCycle.status} />
            <AppButton
              variant="danger"
              size="sm"
              onClick={() => handleClose(activeCycle.reviewCycleId)}
              loading={closeMutation.isPending}
            >
              Close Cycle
            </AppButton>
          </div>
        ) : (
          <p style={{ color: 'var(--text-secondary)' }}>No active review cycle</p>
        )}
      </div>

      <div className="card" style={{ padding: 24 }}>
        <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>
          Create New Cycle
        </h3>
        <div style={{ display: 'grid', gap: 16, maxWidth: 480 }}>
          <div className="form-group">
            <label className="form-label">Cycle Name</label>
            <AppInput
              value={cycleName}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                setCycleName(e.target.value)
              }
              placeholder="e.g. Q1 2026 Review"
            />
          </div>
          <div className="form-group">
            <label className="form-label">Start Date</label>
            <AppInput
              type="date"
              value={startDate}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                setStartDate(e.target.value)
              }
            />
          </div>
          <div className="form-group">
            <label className="form-label">End Date</label>
            <AppInput
              type="date"
              value={endDate}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                setEndDate(e.target.value)
              }
            />
          </div>
          <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
            <AppButton
              variant="primary"
              onClick={handleCreate}
              loading={createMutation.isPending}
              disabled={!cycleName.trim() || !startDate || !endDate}
            >
              Create Cycle
            </AppButton>
          </div>
        </div>
      </div>
    </div>
  );
}
