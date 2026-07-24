import { useState, useCallback } from 'react';
import { useRecordSupervisorRating, useRecordEvaluationRemarks } from '../queries';
import type { RecordSupervisorRatingCommand, RecordEvaluationRemarksCommand } from '../types';

export function usePerformanceForm(performanceEvaluationId: number) {
  const [supervisorRating, setSupervisorRating] = useState<number>(0);
  const [evaluationRemarks, setEvaluationRemarks] = useState('');

  const ratingMutation = useRecordSupervisorRating();
  const remarksMutation = useRecordEvaluationRemarks();

  const submitRating = useCallback(async () => {
    const command: RecordSupervisorRatingCommand = {
      performanceEvaluationId,
      supervisorRating,
    };
    await ratingMutation.mutateAsync(command);
  }, [performanceEvaluationId, supervisorRating, ratingMutation]);

  const submitRemarks = useCallback(async () => {
    const command: RecordEvaluationRemarksCommand = {
      performanceEvaluationId,
      evaluationRemarks,
    };
    await remarksMutation.mutateAsync(command);
  }, [performanceEvaluationId, evaluationRemarks, remarksMutation]);

  return {
    supervisorRating,
    setSupervisorRating,
    evaluationRemarks,
    setEvaluationRemarks,
    submitRating,
    submitRemarks,
    isRatingSubmitting: ratingMutation.isPending,
    isRemarksSubmitting: remarksMutation.isPending,
  };
}
