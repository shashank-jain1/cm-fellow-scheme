import { useState, useEffect } from 'react';
import { AppTextarea, AppSwitch } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { useSubmitPeerFeedback, usePeerFeedback } from '../queries';
import PeerRatingInputs from './PeerRatingInputs';

interface PeerFeedbackFormProps {
  performanceEvaluationId: number;
  userId?: number;
  onSuccess?: () => void;
}

interface RatingField {
  key: string;
  label: string;
  value: number | null;
}

const INITIAL_RATINGS: RatingField[] = [
  { key: 'technical', label: 'Technical Skills', value: null },
  { key: 'communication', label: 'Communication', value: null },
  { key: 'teamwork', label: 'Teamwork', value: null },
  { key: 'leadership', label: 'Leadership', value: null },
  { key: 'overall', label: 'Overall', value: null },
];

export default function PeerFeedbackForm({
  performanceEvaluationId,
  userId,
  onSuccess,
}: PeerFeedbackFormProps) {
  const submitFeedback = useSubmitPeerFeedback();
  const { data: existingFeedback } = usePeerFeedback(performanceEvaluationId);
  const [ratings, setRatings] = useState<RatingField[]>(INITIAL_RATINGS);
  const [comments, setComments] = useState('');
  const [isAnonymous, setIsAnonymous] = useState(false);

  useEffect(() => {
    if (existingFeedback && existingFeedback.length > 0) {
      const fb = existingFeedback[0];
      setRatings([
        { key: 'technical', label: 'Technical Skills', value: fb.technicalRating },
        { key: 'communication', label: 'Communication', value: fb.communicationRating },
        { key: 'teamwork', label: 'Teamwork', value: fb.teamworkRating },
        { key: 'leadership', label: 'Leadership', value: fb.leadershipRating },
        { key: 'overall', label: 'Overall', value: fb.overallRating },
      ]);
      setComments(fb.comments ?? '');
      setIsAnonymous(fb.isAnonymous);
    }
  }, [existingFeedback]);

  const updateRating = (key: string, value: number | null) => {
    setRatings((prev) => prev.map((r) => (r.key === key ? { ...r, value } : r)));
  };

  const getRating = (key: string): number =>
    ratings.find((r) => r.key === key)?.value ?? 0;

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
    onSuccess?.();
  };

  return (
    <div className="card" style={{ padding: 24 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Peer Feedback</h3>
      <PeerRatingInputs ratings={ratings} updateRating={updateRating} />
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
        <AppSwitch checked={isAnonymous} onChange={(e) => setIsAnonymous(!!e.value)} />
        <label className="form-label" style={{ marginBottom: 0 }}>Submit anonymously</label>
      </div>
      <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 20 }}>
        <AppButton
          variant="primary"
          onClick={handleSubmit}
          loading={submitFeedback.isPending}
          disabled={!allRated || submitFeedback.isPending}
        >
          {existingFeedback && existingFeedback.length > 0 ? 'Update Feedback' : 'Submit Feedback'}
        </AppButton>
      </div>
    </div>
  );
}
