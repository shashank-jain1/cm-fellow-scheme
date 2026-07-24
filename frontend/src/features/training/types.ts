export interface ActivityFormData {
  activityType: 'Training' | 'Meeting';
  projectId: number;
  workProjectId: number;
  date: string;
  startTime: string;
  endTime: string;
  mode: string;
  trainingTitle?: string;
  trainingCategory?: string;
  trainingDescription?: string;
  trainerName?: string;
  trainerMobile?: string;
  attendanceRequired?: boolean;
  meetingTitle?: string;
  meetingAgenda?: string;
  meetingDescription?: string;
  conductPersonId?: number;
  coordinatorId?: number;
  momRequired?: boolean;
  remarks?: string;
}

export interface TrainingScheduleDto {
  trainingScheduleId: number;
  activityType: string;
  projectId: number;
  workProjectId: number;
  date: string;
  startTime: string;
  endTime: string;
  mode: string;
  trainingTitle?: string;
  meetingTitle?: string;
  status: string;
}
