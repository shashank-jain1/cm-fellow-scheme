import { useState } from 'react';
import { Rating } from 'primereact/rating';
import { AppTextarea } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { useSubmitSurvey } from '../queries';

interface SatisfactionSurveyProps {
  ticketId: number;
  onSuccess?: () => void;
}

export default function SatisfactionSurvey({ ticketId, onSuccess }: SatisfactionSurveyProps) {
  const submitSurvey = useSubmitSurvey();
  const [rating, setRating] = useState(0);
  const [comments, setComments] = useState('');

  const handleSubmit = async () => {
    if (rating === 0) return;
    await submitSurvey.mutateAsync({
      ticketId,
      rating,
      comments: comments.trim(),
    });
    setRating(0);
    setComments('');
    onSuccess?.();
  };

  return (
    <div className="card" style={{ padding: 24, marginTop: 16 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 4 }}>Rate Your Experience</h3>
      <p style={{ fontSize: 13, color: 'var(--text-muted)', marginBottom: 20 }}>
        How satisfied are you with the resolution of this ticket?
      </p>

      <div className="form-group" style={{ marginBottom: 20 }}>
        <label className="form-label">Rating</label>
        <Rating
          value={rating}
          onChange={(e) => setRating(e.value ?? 0)}
          stars={5}
          cancel={false}
        />
        {rating > 0 && (
          <span style={{ marginLeft: 12, fontSize: 13, color: 'var(--text-secondary)' }}>
            {rating}/5
          </span>
        )}
      </div>

      <div className="form-group" style={{ marginBottom: 20 }}>
        <label className="form-label">Comments (optional)</label>
        <AppTextarea
          value={comments}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setComments(e.target.value)}
          placeholder="Share your feedback about the support experience..."
          rows={3}
        />
      </div>

      <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
        <AppButton
          variant="primary"
          onClick={handleSubmit}
          loading={submitSurvey.isPending}
          disabled={rating === 0 || submitSurvey.isPending}
        >
          Submit Survey
        </AppButton>
      </div>
    </div>
  );
}
