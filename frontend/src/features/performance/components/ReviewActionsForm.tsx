import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { AppButton } from '../../../shared/components/ui';
import { useSubmitReview } from '../queries';
import type { SubmitReviewRequest } from '../types';
import ReviewRatingInput from './ReviewRatingInput';
import ReviewRemarksField from './ReviewRemarksField';

interface Props {
  reviewLevel: string;
  reviewStatus: string;
  evaluationId: number;
}

export default function ReviewActionsForm({
  reviewLevel,
  reviewStatus,
  evaluationId,
}: Props) {
  const submitReviewMutation = useSubmitReview();
  const toast = useRef<Toast>(null);
  const [reviewAction, setReviewAction] = useState<'Submit' | 'Approve' | 'Reject'>('Submit');
  const [reviewRemarks, setReviewRemarks] = useState('');
  const [performedBy, setPerformedBy] = useState('');

  const canSubmit =
    (reviewLevel === 'Draft' || reviewLevel === 'Fellow') &&
    reviewStatus !== 'Approved';
  const canApprove = reviewLevel === 'Coordinator' || reviewLevel === 'Admin';

  if (reviewStatus === 'Approved') return null;

  const handleReviewSubmit = async () => {
    if (!performedBy.trim()) {
      toast.current?.show({
        severity: 'warn',
        summary: 'Required',
        detail: 'Please enter your name',
      });
      return;
    }
    const command: SubmitReviewRequest = {
      action: reviewAction,
      performedBy,
      remarks: reviewRemarks || undefined,
    };
    try {
      await submitReviewMutation.mutateAsync({ id: evaluationId, command });
      toast.current?.show({
        severity: 'success',
        summary: 'Success',
        detail: `Review ${reviewAction.toLowerCase()}d successfully`,
      });
      setReviewRemarks('');
    } catch {
      toast.current?.show({
        severity: 'error',
        summary: 'Error',
        detail: 'Failed to submit review',
      });
    }
  };

  return (
    <>
      <Toast ref={toast} />
      <div style={{ borderTop: '1px solid var(--border-color)', paddingTop: 16 }}>
        <div className="form-grid">
          <ReviewRatingInput
            reviewAction={reviewAction}
            setReviewAction={setReviewAction}
            performedBy={performedBy}
            setPerformedBy={setPerformedBy}
            canSubmit={canSubmit}
            canApprove={canApprove}
          />
          <ReviewRemarksField
            reviewRemarks={reviewRemarks}
            setReviewRemarks={setReviewRemarks}
          />
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 12 }}>
          <AppButton
            onClick={handleReviewSubmit}
            loading={submitReviewMutation.isPending}
          >
            {reviewAction === 'Submit'
              ? 'Submit for Review'
              : reviewAction === 'Approve'
                ? 'Approve'
                : 'Reject'}
          </AppButton>
        </div>
      </div>
    </>
  );
}
