const certificateUrls = {
  base: 'certificates',
  list: () => 'certificates',
  detail: (id: number) => `certificates/status?CertificateId=${id}`,
  apply: () => 'certificates',
  review: () => 'certificates/review',
  generate: (id: number) => `certificates/${id}/generate`,
  download: (id: number) => `certificates/${id}/download`,
  generateCompletion: (applicantId: number) => `certificates/${applicantId}/completion-certificate`,
  generateExperience: (applicantId: number) => `certificates/${applicantId}/experience-letter`,
  verify: (certNumber: string) => `certificates/verify/${encodeURIComponent(certNumber)}`,
  exitReadiness: () => 'exit/readiness',
  exitCompliance: () => 'exit/compliance',
  exitCloseArchive: (id: number) => `exit/${id}/close-archive`,
  exitInterview: () => 'exit-interview',
  exitInterviewByUser: (userId: number) => `exit-interview?userAccountId=${userId}`,
};

export default certificateUrls;
