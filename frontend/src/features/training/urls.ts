const trainingUrls = {
  base: 'training',
  sessions: () => 'training/sessions',
  meetings: () => 'training/meetings',
  createSession: () => 'training/sessions',
  createMeeting: () => 'training/meetings',
  completions: () => 'training/completions',
  createCompletion: () => 'training/completions',
  material: (id: number) => `training/materials/${id}`,
  meetingDetail: (id: number) => `training/meetings/${id}`,
  meetingAttachment: (id: number) => `training/meetings/${id}/attachment`,
  meetingMom: (id: number) => `training/meetings/${id}/mom`,
};

export default trainingUrls;
