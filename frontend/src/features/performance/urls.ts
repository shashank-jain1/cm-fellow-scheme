const performanceUrls = {
  base: 'performance',
  summary: () => `performance/summary`,
  detail: (id: number) => `performance/${id}`,
  recordRating: () => `performance/supervisor-rating`,
  recordRemarks: () => `performance/evaluation-remarks`,
};

export default performanceUrls;
