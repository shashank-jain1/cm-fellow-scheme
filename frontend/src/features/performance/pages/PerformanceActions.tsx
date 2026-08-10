import ReviewActionsForm from '../components/ReviewActionsForm';
import SupervisorRatingInput from '../components/SupervisorRatingInput';
import EvaluationRemarksForm from '../components/EvaluationRemarksForm';

interface PerformanceActionsProps {
  reviewLevel: string;
  reviewStatus: string;
  evaluationId: number;
  supervisorRating: number;
  setSupervisorRating: (v: number) => void;
  evaluationRemarks: string;
  setEvaluationRemarks: (v: string) => void;
  submitRating: () => void;
  submitRemarks: () => void;
  isRatingSubmitting: boolean;
  isRemarksSubmitting: boolean;
  initialRemarks?: string | null;
}

export default function PerformanceActions({
  reviewLevel,
  reviewStatus,
  evaluationId,
  supervisorRating,
  setSupervisorRating,
  evaluationRemarks,
  setEvaluationRemarks,
  submitRating,
  submitRemarks,
  isRatingSubmitting,
  isRemarksSubmitting,
  initialRemarks,
}: PerformanceActionsProps) {
  return (
    <>
      <div className="card" style={{ padding: 'var(--space-5)', marginBottom: 'var(--space-4)' }}>
        <ReviewActionsForm
          reviewLevel={reviewLevel}
          reviewStatus={reviewStatus}
          evaluationId={evaluationId}
        />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 'var(--space-4)' }}>
        <SupervisorRatingInput
          value={supervisorRating}
          onChange={setSupervisorRating}
          onSubmit={submitRating}
          isSubmitting={isRatingSubmitting}
        />
        <EvaluationRemarksForm
          value={evaluationRemarks}
          onChange={setEvaluationRemarks}
          onSubmit={submitRemarks}
          isSubmitting={isRemarksSubmitting}
          initialRemarks={initialRemarks}
        />
      </div>
    </>
  );
}
