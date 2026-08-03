import ApiService from '../../services/ApiService';
import type { SeedLog } from './types';
import { log } from './seedHelpers';
import type { SeedFn } from './seedMasters';

const seedPerformance: SeedFn = async (appendLog) => {
  let count = 0;

  const perfRes = await ApiService.get<Array<{ performanceEvaluationId: number }>>('performance/list');
  const evals = perfRes.data ?? [];

  if (evals.length === 0) {
    appendLog(log('Performance', 'No performance evaluations found. They are auto-created from work allocation data.', 'info'));
    return 0;
  }

  for (const e of evals) {
    await ApiService.put<void>('performance/rating', {
      performanceEvaluationId: e.performanceEvaluationId,
      supervisorRating: 7.5,
      evaluatedBy: 'Admin',
    });
    count++;
    appendLog(log('Performance', `Recorded rating for evaluation ${e.performanceEvaluationId}`, 'success'));

    await ApiService.put<void>('performance/remarks', {
      performanceEvaluationId: e.performanceEvaluationId,
      evaluationRemarks: 'Good performance. Consistent attendance and quality work.',
      evaluatedBy: 'Admin',
    });
    appendLog(log('Performance', `Recorded remarks for evaluation ${e.performanceEvaluationId}`, 'success'));

    await ApiService.post<void>(`performance/${e.performanceEvaluationId}/calculate-score`, {});
    appendLog(log('Performance', `Calculated score for evaluation ${e.performanceEvaluationId}`, 'success'));
  }

  return count;
};

export default seedPerformance;
