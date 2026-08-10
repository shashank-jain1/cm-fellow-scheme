import { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { AppTextarea, AppSwitch } from '../../../shared/components/forms';
import { AppButton } from '../../../shared/components/ui';
import { ToastService } from '../../../shared/utils/toast';
import { useSubmitExitInterview, useExitInterview } from '../queries';
import ExitInterviewRatingList from './ExitInterviewRatingList';

const ratingKeys = ['overallExperience', 'workEnvironment', 'learningOpportunities', 'teamCollaboration'] as const;

type RatingKey = (typeof ratingKeys)[number];

interface RatingField {
  key: RatingKey;
  label: string;
  value: number | null;
}

const initialRatings: RatingField[] = [
  { key: 'overallExperience', label: 'Overall Experience', value: null },
  { key: 'workEnvironment', label: 'Work Environment', value: null },
  { key: 'learningOpportunities', label: 'Learning Opportunities', value: null },
  { key: 'teamCollaboration', label: 'Team Collaboration', value: null },
];

export default function ExitInterviewForm() {
  const [searchParams] = useSearchParams();
  const userAccountId = Number(searchParams.get('userId') ?? 0);
  const { data: existing } = useExitInterview(userAccountId);
  const submitMutation = useSubmitExitInterview();
  const [ratings, setRatings] = useState<RatingField[]>(initialRatings);
  const [improvementSuggestions, setImprovementSuggestions] = useState('');
  const [whatWorkedWell, setWhatWorkedWell] = useState('');
  const [wouldRecommend, setWouldRecommend] = useState(false);

  useEffect(() => {
    if (existing) {
      setRatings((prev) => prev.map((r) => ({ ...r, value: existing[r.key] ?? null })));
      setImprovementSuggestions(existing.improvementSuggestions ?? '');
      setWhatWorkedWell(existing.whatWorkedWell ?? '');
      setWouldRecommend(existing.wouldRecommend ?? false);
    }
  }, [existing]);

  const updateRating = (key: string, value: number | null) => {
    setRatings((prev) => prev.map((r) => (r.key === key ? { ...r, value } : r)));
  };

  const ratingFor = (key: RatingKey): number => ratings.find((r) => r.key === key)?.value ?? 0;
  const allRated = ratings.every((r) => r.value !== null && r.value > 0);

  const handleSubmit = async () => {
    if (!allRated) return;
    try {
      await submitMutation.mutateAsync({
        userAccountId,
        overallExperience: ratingFor('overallExperience'),
        workEnvironment: ratingFor('workEnvironment'),
        learningOpportunities: ratingFor('learningOpportunities'),
        teamCollaboration: ratingFor('teamCollaboration'),
        improvementSuggestions: improvementSuggestions.trim(),
        whatWorkedWell: whatWorkedWell.trim(),
        wouldRecommend,
      });
      ToastService.success('Exit interview submitted successfully!');
      setRatings(initialRatings);
      setImprovementSuggestions('');
      setWhatWorkedWell('');
      setWouldRecommend(false);
    } catch {
      ToastService.error('Failed to submit exit interview');
    }
  };

  return (
    <div className="card" style={{ padding: 24, maxWidth: 640 }}>
      <h3 style={{ fontSize: 16, fontWeight: 600, marginBottom: 20 }}>Exit Interview</h3>
      <ExitInterviewRatingList ratings={ratings} onChange={updateRating} />
      <div className="form-group" style={{ marginTop: 20 }}>
        <label className="form-label" style={{ fontWeight: 600 }}>Suggestions for Improvement</label>
        <AppTextarea
          value={improvementSuggestions}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setImprovementSuggestions(e.target.value)}
          placeholder="What could be improved..."
          rows={4}
        />
      </div>
      <div className="form-group" style={{ marginTop: 16 }}>
        <label className="form-label" style={{ fontWeight: 600 }}>What Worked Well</label>
        <AppTextarea
          value={whatWorkedWell}
          onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => setWhatWorkedWell(e.target.value)}
          placeholder="Share positive experiences..."
          rows={4}
        />
      </div>
      <div className="form-group" style={{ marginTop: 16, display: 'flex', alignItems: 'center', gap: 12 }}>
        <AppSwitch checked={wouldRecommend} onChange={(e) => setWouldRecommend(!!e.value)} />
        <label className="form-label" style={{ marginBottom: 0 }}>
          I would recommend this program to others
        </label>
      </div>
      <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 24 }}>
        <AppButton
          variant="primary"
          onClick={handleSubmit}
          loading={submitMutation.isPending}
          disabled={!allRated || submitMutation.isPending}
        >
          Submit Exit Interview
        </AppButton>
      </div>
    </div>
  );
}
