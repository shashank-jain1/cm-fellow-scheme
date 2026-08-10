const trainingUrls = {
  base: 'training',
  sessions: () => 'training/sessions',
  meetings: () => 'training/meetings',
  createSession: () => 'training/sessions',
  createMeeting: () => 'training/meetings',
  completions: () => 'training/completions',
  createCompletion: () => 'training/completions',
  sessionStatus: (id: number) => `training/sessions/${id}/status`,
  sessionMaterials: (trainingScheduleId: number) => `training/sessions/${trainingScheduleId}/materials`,
  downloadMaterial: (materialId: number) => `training/sessions/materials/${materialId}/download`,
  meetingDetail: (id: number) => `training/meetings/${id}`,
  meetingAttachment: (id: number) => `training/meetings/${id}/attachments`,
  meetingMom: (id: number) => `training/meetings/${id}/mom`,
};

export default trainingUrls;
