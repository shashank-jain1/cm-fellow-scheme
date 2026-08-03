import { AppButton } from '../../../shared/components/ui';

interface ReviewRatingInputProps {
  reviewAction: 'Submit' | 'Approve' | 'Reject';
  setReviewAction: (action: 'Submit' | 'Approve' | 'Reject') => void;
  performedBy: string;
  setPerformedBy: (value: string) => void;
  canSubmit: boolean;
  canApprove: boolean;
}

export default function ReviewRatingInput({
  reviewAction,
  setReviewAction,
  performedBy,
  setPerformedBy,
  canSubmit,
  canApprove,
}: ReviewRatingInputProps) {
  return (
    <>
      <div className="form-field">
        <label>Your Name *</label>
        <input
          value={performedBy}
          onChange={(e) => setPerformedBy(e.target.value)}
          placeholder="Enter your name"
          className="p-inputtext p-component"
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
    </>
  );
}
