import { AppTextarea } from '../../../shared/components/forms';

interface ReviewRemarksFieldProps {
  reviewRemarks: string;
  setReviewRemarks: (value: string) => void;
}

export default function ReviewRemarksField({
  reviewRemarks,
  setReviewRemarks,
}: ReviewRemarksFieldProps) {
  return (
    <div className="form-field full-width">
      <label>Remarks</label>
      <AppTextarea
        value={reviewRemarks}
        onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) =>
          setReviewRemarks(e.target.value)
        }
        rows={2}
        placeholder="Add remarks (optional)"
        style={{ width: '100%' }}
      />
    </div>
  );
}
