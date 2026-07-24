const certificateUrls = {
  base: 'certificate',
  list: () => `certificate`,
  detail: (id: number) => `certificate/${id}`,
  apply: () => `certificate/apply`,
  approve: (id: number) => `certificate/${id}/approve`,
  reject: (id: number) => `certificate/${id}/reject`,
  download: (id: number) => `certificate/${id}/download`,
  exitChecklist: (applicantId: number) => `certificate/exit-checklist/${applicantId}`,
};

export default certificateUrls;
