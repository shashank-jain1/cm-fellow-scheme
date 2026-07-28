export const REGISTRATION_URLS = {
  FELLOWS: 'registrations',
  FELLOW_BY_ID: (id: number) => `registrations/${id}`,
  DIVISIONS: 'masters/locations/divisions',
  DISTRICTS: 'masters/locations/districts',
  BLOCKS: 'masters/locations/blocks',
  PROJECTS: 'masters/projects',
  FORGOT_PASSWORD: 'user-accounts/forgot-password',
  RESET_PASSWORD: 'user-accounts/reset-password',
};
