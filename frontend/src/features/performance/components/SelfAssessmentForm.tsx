import { useState, useEffect } from 'react';
import { AppButton } from '../../../shared/components/ui';
import { ToastService } from '../../../shared/utils/toast';
import { useSubmitSelfAssessment, useSelfAssessment } from '../queries';
import SelfAssessmentRatingInput from './SelfAssessmentRatingInput';
import SelfAssessmentTextArea from './SelfAssessmentTextArea';

export default function SelfAssessmentForm() {
  const submitMutation = useSubmitSelfAssessment();
  const { data: existing } = useSelfAssessment();
  const [strengths, setStrengths] = useState('');
  const [improvements, setImprovements] = useState('');
  const [goalsAchieved, setGoalsAchieved] = useState('');
  const [goalsMissed, setGoalsMissed] = useState('');
  const [trainingFeedback, setTrainingFeedback] = useState('');
  const [overallRating, setOverallRating] = useState<number | null>(null);

  useEffect(() => {
    if (existing) {
      setStrengths(existing.strengths ?? '');
      setImprovements(existing.improvements ?? '');
      setGoalsAchieved(existing.goalsAchieved ?? '');
      setGoalsMissed(existing.goalsMissed ?? '');
      setTrainingFeedback(existing.trainingFeedback ?? '');
      setOverallRating(existing.overallRating ?? null);
    }
  }, [existing]);

  const isValid = overallRating !== null && overallRating > 0;

  const handleSubmit = async () => {
    if (!isValid) return;
    try {
      await submitMutation.mutateAsync({
        strengths: strengths.trim(),
        improvements: improvements.trim(),
        goalsAchieved: goalsAchieved.trim(),
        goalsMissed: goalsMissed.trim(),
        trainingFeedback: trainingFeedback.trim(),
        overallRating,
      });
      ToastService.success('Self-assessment submitted successfully!');
    } catch {
      ToastService.error('Failed to submit self-assessment');
    }
  };

  return (
    <div>
      <div className="page-header">
        <div>
          <h1>Self-Assessment</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: 4 }}>Evaluate your performance this period</p>
        </div>
      </div>
      <div className="glass-card fade-in" style={{ padding: 24, maxWidth: 640 }}>
        <div style={{ display: 'grid', gap: 20 }}>
          <SelfAssessmentTextArea label="Strengths" value={strengths} onChange={setStrengths} placeholder="What did you do well?" />
          <SelfAssessmentTextArea label="Areas for Improvement" value={improvements} onChange={setImprovements} placeholder="What could be improved?" />
          <SelfAssessmentTextArea label="Goals Achieved" value={goalsAchieved} onChange={setGoalsAchieved} placeholder="Which goals were met?" />
          <SelfAssessmentTextArea label="Goals Missed" value={goalsMissed} onChange={setGoalsMissed} placeholder="Which goals were missed?" />
          <SelfAssessmentTextArea label="Training Feedback" value={trainingFeedback} onChange={setTrainingFeedback} placeholder="Feedback on training received" />
          <SelfAssessmentRatingInput value={overallRating} onChange={setOverallRating} />
        </div>
        <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 24 }}>
          <AppButton
            variant="primary"
            onClick={handleSubmit}
            loading={submitMutation.isPending}
            disabled={!isValid || submitMutation.isPending}
          >
            {existing ? 'Update Self-Assessment' : 'Submit Self-Assessment'}
          </AppButton>
        </div>
      </div>
    </div>
  );
}
