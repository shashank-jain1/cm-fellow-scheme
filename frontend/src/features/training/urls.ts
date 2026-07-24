const trainingUrls = {
  base: 'training',
  schedules: () => 'training/schedules',
  scheduleDetail: (id: number) => `training/schedules/${id}`,
  createSchedule: () => 'training/schedules',
  updateSchedule: (id: number) => `training/schedules/${id}`,
  deleteSchedule: (id: number) => `training/schedules/${id}`,
};

export default trainingUrls;
