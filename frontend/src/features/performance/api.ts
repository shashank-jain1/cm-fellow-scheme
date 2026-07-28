import ApiService from '../../services/ApiService';
import performanceUrls from './urls';
import type { PerformanceSummaryDto, RecordSupervisorRatingCommand, RecordEvaluationRemarksCommand } from './types';

export async function fetchPerformanceSummary(): Promise<PerformanceSummaryDto[]> {
  const res = await ApiService.get<PerformanceSummaryDto[]>(performanceUrls.summary());
  return res.data ?? [];
}

export async function fetchPerformanceDetail(id: number): Promise<PerformanceSummaryDto> {
  const res = await ApiService.get<PerformanceSummaryDto>(performanceUrls.detail(id));
  return res.data!;
}

export async function recordSupervisorRating(command: RecordSupervisorRatingCommand): Promise<void> {
  await ApiService.put(performanceUrls.recordRating(), command);
}

export async function recordEvaluationRemarks(command: RecordEvaluationRemarksCommand): Promise<void> {
  await ApiService.put(performanceUrls.recordRemarks(), command);
}

export async function calculatePerformanceScore(performanceEvaluationId: number): Promise<void> {
  await ApiService.post(`performance/${performanceEvaluationId}/calculate-score`, {});
}
