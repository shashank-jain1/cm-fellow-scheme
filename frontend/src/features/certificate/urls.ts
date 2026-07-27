const certificateUrls = {
  base: 'certificates',
  list: () => 'certificates',
  detail: (id: number) => `certificates/status?CertificateId=${id}`,
  apply: () => 'certificates',
  review: () => 'certificates/review',
  download: (id: number) => `certificates/status?CertificateId=${id}`,
};

export default certificateUrls;
