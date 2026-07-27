const performanceUrls = {
  base: 'performance',
  summary: () => 'performance/summary',
  list: () => 'performance/list',
  detail: (id: number) => `performance/summary?applicantId=${id}`,
  recordRating: () => 'performance/rating',
  recordRemarks: () => 'performance/remarks',
};

export default performanceUrls;
