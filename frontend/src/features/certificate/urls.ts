const certificateUrls = {
  base: 'certificates',
  list: () => 'certificates',
  detail: (id: number) => `certificates/status?CertificateId=${id}`,
  apply: () => 'certificates',
  review: () => 'certificates/review',
  generate: (id: number) => `certificates/${id}/generate`,
  download: (id: number) => `certificates/${id}/download`,
  generateCompletion: () => 'certificates/generate-completion',
  generateExperience: () => 'certificates/generate-experience',
  verify: () => 'certificates/verify',
  exitReadiness: () => 'exit/readiness',
  exitCompliance: (id: number) => `exit/${id}/compliance`,
  exitCloseArchive: (id: number) => `exit/${id}/close-archive`,
  exitInterview: () => 'registration/exit-interview',
  exitInterviewByUser: (userId: number) => `registration/exit-interview?userAccountId=${userId}`,
};

export default certificateUrls;
