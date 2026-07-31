const trainingUrls = {
  base: 'training',
  sessions: () => 'training/sessions',
  meetings: () => 'training/meetings',
  createSession: () => 'training/sessions',
  createMeeting: () => 'training/meetings',
  completions: () => 'training/completions',
  createCompletion: () => 'training/completions',
  material: (id: number) => `training/materials/${id}`,
  uploadMaterial: () => 'training/materials',
};

export default trainingUrls;
