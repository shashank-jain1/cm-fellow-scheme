import { useState } from 'react';
import { Toast } from 'primereact/toast';
import { useRef } from 'react';
import { AppInput, AppTextarea } from '../../../shared/components/forms';
import { useSubmitReview } from '../queries';
import { AppButton } from '../../../shared/components/ui';
import type { SubmitReviewRequest } from '../types';

interface Props {
  reviewLevel: string;
  reviewStatus: string;
  evaluationId: number;
}

export default function ReviewActionsForm({ reviewLevel, reviewStatus, evaluationId }: Props) {
  const submitReviewMutation = useSubmitReview();
  const toast = useRef<Toast>(null);

  const [reviewAction, setReviewAction] = useState<'Submit' | 'Approve' | 'Reject'>('Submit');
  const [reviewRemarks, setReviewRemarks] = useState('');
  const [performedBy, setPerformedBy] = useState('');

  const canSubmit = (reviewLevel === 'Draft' || reviewLevel === 'Fellow') && reviewStatus !== 'Approved';
  const canApprove = reviewLevel === 'Coordinator' || reviewLevel === 'Admin';

  if (reviewStatus === 'Approved') {
    return null;
  }

  const handleReviewSubmit = async () => {
    if (!performedBy.trim()) {
      toast.current?.show({ severity: 'warn', summary: 'Required', detail: 'Please enter your name' });
      return;
    }

    const command: SubmitReviewRequest = {
      action: reviewAction,
      performedBy,
      remarks: reviewRemarks || undefined,
    };

    try {
      await submitReviewMutation.mutateAsync({ id: evaluationId, command });
      toast.current?.show({ severity: 'success', summary: 'Success', detail: `Review ${reviewAction.toLowerCase()}d successfully` });
      setReviewRemarks('');
    } catch {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: 'Failed to submit review' });
    }
  };

  return (
    <>
      <Toast ref={toast} />
      <div style={{ borderTop: '1px solid var(--border-color)', paddingTop: 16 }}>
        <div className="form-grid">
          <div className="form-field">
            <label>Your Name *</label>
            <AppInput
              value={performedBy}
              onChange={(e) => setPerformedBy(e.target.value)}
              placeholder="Enter your name"
              style={{ width: '100%' }}
            />
          </div>
          <div className="form-field">
            <label>Action</label>
            <div style={{ display: 'flex', gap: 8 }}>
              {canSubmit && (
                <AppButton
                  size="sm"
                  onClick={() => setReviewAction('Submit')}
                  variant={reviewAction === 'Submit' ? undefined : 'secondary'}
                >
                  Submit
                </AppButton>
              )}
              {canApprove && (
                <>
                  <AppButton
                    size="sm"
                    onClick={() => setReviewAction('Approve')}
                    variant={reviewAction === 'Approve' ? undefined : 'secondary'}
                  >
                    Approve
                  </AppButton>
                  <AppButton
                    size="sm"
                    variant="danger"
                    onClick={() => setReviewAction('Reject')}
                  >
                    Reject
                  </AppButton>
                </>
              )}
            </div>
          </div>
          <div className="form-field full-width">
            <label>Remarks</label>
            <AppTextarea
              value={reviewRemarks}
              onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setReviewRemarks(e.target.value)}
              rows={2}
              placeholder="Add remarks (optional)"
              style={{ width: '100%' }}
            />
          </div>
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 12 }}>
          <AppButton
            onClick={handleReviewSubmit}
            loading={submitReviewMutation.isPending}
          >
            {reviewAction === 'Submit' ? 'Submit for Review' : reviewAction === 'Approve' ? 'Approve' : 'Reject'}
          </AppButton>
        </div>
      </div>
    </>
  );
}
