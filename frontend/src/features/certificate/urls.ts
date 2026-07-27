const certificateUrls = {
  base: 'certificates',
  list: () => 'certificates/status',
  detail: () => `certificates/status`,
  apply: () => 'certificates',
  review: () => 'certificates/review',
  download: () => `certificates/status`,
};

export default certificateUrls;
