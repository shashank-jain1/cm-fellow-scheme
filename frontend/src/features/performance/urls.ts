const performanceUrls = {
  base: 'performance',
  summary: () => 'performance/summary',
  list: () => 'performance',
  detail: (id: number) => `performance/summary?applicantId=${id}`,
  recordRating: () => 'performance/rating',
  recordRemarks: () => 'performance/remarks',
  submitReview: (id: number) => `performance/${id}/review`,
  reviewHistory: (id: number) => `performance/${id}/review-history`,
  goals: () => 'performance/goals',
  goalsByUser: (userId: number) => `performance/goals/user/${userId}`,
  goalStatus: (goalId: number) => `performance/goals/${goalId}/status`,
  improvementPlans: () => 'improvement-plans',
  improvementPlansByUser: (userId: number) => `improvement-plans/user/${userId}`,
  peerFeedback: () => 'peer-feedback',
  peerFeedbackByEvaluation: (evaluationId: number) => `peer-feedback/evaluation/${evaluationId}`,
  selfAssessment: () => 'performance/self-assessment',
  selfAssessmentByUser: () => 'performance/self-assessment',
  reviewCycles: () => 'performance/review-cycle',
  activeReviewCycle: () => 'performance/review-cycle',
  closeReviewCycle: (id: number) => `performance/review-cycle/${id}/close`,
};

export default performanceUrls;
