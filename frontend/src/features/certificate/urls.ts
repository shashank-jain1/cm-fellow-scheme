const certificateUrls = {
  base: 'certificates',
  list: () => 'certificates',
  detail: (id: number) => `certificates/status?CertificateId=${id}`,
  apply: () => 'certificates',
  review: () => 'certificates/review',
  generate: (id: number) => `certificates/${id}/generate`,
  download: (id: number) => `certificates/${id}/download`,
  exitReadiness: () => 'exit/readiness',
  exitCloseArchive: (id: number) => `exit/${id}/close-archive`,
  exitInterview: () => 'registration/exit-interview',
};

export default certificateUrls;
