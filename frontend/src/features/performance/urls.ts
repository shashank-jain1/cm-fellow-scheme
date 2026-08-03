const performanceUrls = {
  base: 'performance',
  summary: () => 'performance/summary',
  list: () => 'performance/list',
  detail: (id: number) => `performance/summary?applicantId=${id}`,
  recordRating: () => 'performance/rating',
  recordRemarks: () => 'performance/remarks',
  submitReview: (id: number) => `performance/${id}/review`,
  reviewHistory: (id: number) => `performance/${id}/review-history`,
  goals: () => 'performance/goals',
  goalsByUser: (userId: number) => `performance/goals/by-user/${userId}`,
  goalStatus: (goalId: number) => `performance/goals/${goalId}/status`,
  improvementPlans: () => 'improvement-plans',
  improvementPlansByUser: (userId: number) => `improvement-plans/by-user/${userId}`,
  peerFeedback: () => 'peer-feedback',
  peerFeedbackByUser: (userId: number) => `peer-feedback/by-user/${userId}`,
  selfAssessment: () => 'performance/self-assessment',
  selfAssessmentByUser: () => 'performance/self-assessment/current',
  reviewCycles: () => 'performance/review-cycles',
  activeReviewCycle: () => 'performance/review-cycles/active',
  closeReviewCycle: (id: number) => `performance/review-cycles/${id}/close`,
};

export default performanceUrls;
