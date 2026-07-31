import { useState } from 'react';
import { InputNumber } from 'primereact/inputnumber';
import { AppTextarea, AppSwitch } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { useSubmitPeerFeedback } from '../queries';

interface PeerFeedbackFormProps {
  performanceEvaluationId: number;
  onSuccess?: () => void;
}

interface RatingField {
  key: string;
  label: string;
  value: number | null;
}

export default function PeerFeedbackForm({ performanceEvaluationId, onSuccess }: PeerFeedbackFormProps) {
  const submitFeedback = useSubmitPeerFeedback();

  const [ratings, setRatings] = useState<RatingField[]>([
    { key: 'technical', label: 'Technical Skills', value: null },
    { key: 'communication', label: 'Communication', value: null },
    { key: 'teamwork', label: 'Teamwork', value: null },
    { key: 'leadership', label: 'Leadership', value: null },
    { key: 'overall', label: 'Overall', value: null },
  ]);
  const [comments, setComments] = useState('');
  const [isAnonymous, setIsAnonymous] = useState(false);

  const updateRating = (key: string, value: number | null) => {
    setRatings((prev) => prev.map((r) => (r.key === key ? { ...r, value } : r)));
  };

  const getRating = (key: string): number => {
    const found = ratings.find((r) => r.key === key);
    return found?.value ?? 0;
  };

  const allRated = ratings.every((r) => r.value !== null && r.value > 0);

  const handleSubmit = async () => {
    if (!allRated) return;
    await submitFeedback.mutateAsync({
      performanceEvaluationId,
      technicalRating: getRating('technical'),
      communicationRating: getRating('communication'),
      teamworkRating: getRating('teamwork'),
      leadershipRating: getRating('leadership'),
      overallRating: getRating('overall'),
      comments: comments.trim(),
      isAnonymous,
    });
    setRatings((prev) => prev.map((r) => ({ ...r, value: null })));
    setComments('');
    setIsAnonymous(false);
    onSuccess?.();
  };

  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Peer Feedback</h3>

      <div style={{ display: 'grid', gap: 20 }}>
        {ratings.map((field) => (
          <div key={field.key} className="form-group" style={{ display: 'flex', alignItems: 'center', gap: 16 }}>
            <label className="form-label" style={{ minWidth: 160, marginBottom: 0 }}>
              {field.label}
            </label>
            <InputNumber
              value={field.value}
              onValueChange={(e) => updateRating(field.key, e.value ?? null)}
              min={1}
              max={5}
              showButtons
              buttonLayout="horizontal"
              decrementButtonClassName="btn btn-secondary"
              incrementButtonClassName="btn btn-secondary"
              incrementButtonIcon="pi pi-plus"
              decrementButtonIcon="pi pi-minus"
              style={{ width: 140 }}
            />
            <span style={{ fontSize: 12, color: 'var(--text-muted)' }}>(1-5 scale)</span>
          </div>
        ))}
      </div>

      <div className="form-group" style={{ marginTop: 20 }}>
        <label className="form-label">Comments</label>
        <AppTextarea
          value={comments}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setComments(e.target.value)}
          placeholder="Provide detailed feedback..."
          rows={4}
        />
      </div>

      <div className="form-group" style={{ marginTop: 12, display: 'flex', alignItems: 'center', gap: 12 }}>
        <AppSwitch
          checked={isAnonymous}
          onChange={(e) => setIsAnonymous(!!e.value)}
        />
        <label className="form-label" style={{ marginBottom: 0 }}>Submit anonymously</label>
      </div>

      <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 20 }}>
        <AppButton
          variant="primary"
          onClick={handleSubmit}
          loading={submitFeedback.isPending}
          disabled={!allRated || submitFeedback.isPending}
        >
          Submit Feedback
        </AppButton>
      </div>
    </div>
  );
}
