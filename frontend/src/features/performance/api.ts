import ApiService from '../../services/ApiService';
import performanceUrls from './urls';
import type {
  PerformanceSummaryDto,
  RecordSupervisorRatingCommand,
  RecordEvaluationRemarksCommand,
  SubmitReviewRequest,
  ReviewHistoryDto,
  CreateGoalCommand,
  UpdateGoalStatusCommand,
  PerformanceGoalDto,
  CreateImprovementPlanCommand,
  ImprovementPlanDto,
  SubmitPeerFeedbackCommand,
  SubmitSelfAssessmentCommand,
  ReviewCycleDto,
  CreateReviewCycleCommand,
  PeerFeedbackDto,
  SelfAssessmentDto,
} from './types';

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
  await ApiService.put(`performance/${performanceEvaluationId}/calculate-score`, {});
}

export async function submitReview(id: number, command: SubmitReviewRequest): Promise<void> {
  await ApiService.put(performanceUrls.submitReview(id), command);
}

export async function fetchReviewHistory(id: number): Promise<ReviewHistoryDto[]> {
  const res = await ApiService.get<ReviewHistoryDto[]>(performanceUrls.reviewHistory(id));
  return res.data ?? [];
}

export async function createGoal(command: CreateGoalCommand): Promise<PerformanceGoalDto> {
  const res = await ApiService.post<PerformanceGoalDto>(performanceUrls.goals(), command);
  return res.data!;
}

export async function fetchGoalsByUser(userId: number): Promise<PerformanceGoalDto[]> {
  const res = await ApiService.get<PerformanceGoalDto[]>(performanceUrls.goalsByUser(userId));
  return res.data ?? [];
}

export async function updateGoalStatus(goalId: number, command: UpdateGoalStatusCommand): Promise<void> {
  await ApiService.put(performanceUrls.goalStatus(goalId), command);
}

export async function createImprovementPlan(command: CreateImprovementPlanCommand): Promise<ImprovementPlanDto> {
  const res = await ApiService.post<ImprovementPlanDto>(performanceUrls.improvementPlans(), command);
  return res.data!;
}

export async function fetchImprovementPlansByUser(userId: number): Promise<ImprovementPlanDto[]> {
  const res = await ApiService.get<ImprovementPlanDto[]>(performanceUrls.improvementPlansByUser(userId));
  return res.data ?? [];
}

export async function submitPeerFeedback(command: SubmitPeerFeedbackCommand): Promise<void> {
  await ApiService.post(performanceUrls.peerFeedback(), command);
}

export async function submitSelfAssessment(command: SubmitSelfAssessmentCommand): Promise<void> {
  await ApiService.post(performanceUrls.selfAssessment(), command);
}

export async function getPeerFeedback(evaluationId: number): Promise<PeerFeedbackDto[]> {
  const res = await ApiService.get<PeerFeedbackDto[]>(performanceUrls.peerFeedbackByEvaluation(evaluationId));
  return res.data ?? [];
}

export async function getSelfAssessment(): Promise<SelfAssessmentDto | null> {
  const res = await ApiService.getOptional<SelfAssessmentDto>(performanceUrls.selfAssessmentByUser());
  return res.data ?? null;
}

export async function createReviewCycle(command: CreateReviewCycleCommand): Promise<ReviewCycleDto> {
  const res = await ApiService.post<ReviewCycleDto>(performanceUrls.reviewCycles(), command);
  return res.data!;
}

export async function getActiveReviewCycle(): Promise<ReviewCycleDto | null> {
  const res = await ApiService.getOptional<ReviewCycleDto>(performanceUrls.activeReviewCycle());
  return res.data ?? null;
}

export async function closeReviewCycle(id: number): Promise<void> {
  await ApiService.put(performanceUrls.closeReviewCycle(id), {});
}
