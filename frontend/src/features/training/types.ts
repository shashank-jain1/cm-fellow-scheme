export interface ActivityFormData {
  activityType: 'Training' | 'Meeting';
  projectId: number;
  workProjectId: number;
  date: string;
  startTime: string;
  endTime: string;
  mode: string;
  applicableDivisionIds: number[];
  applicableDistrictIds: number[];
  applicableBlockIds: number[];
  remarks?: string;
  trainingTitle?: string;
  trainingCategory?: string;
  trainingDescription?: string;
  targetUserTypes: string[];
  trainerName?: string;
  trainerMobile?: string;
  attendanceRequired?: boolean;
  trainingMaterialFile?: File | null;
  meetingTitle?: string;
  meetingAgenda?: string;
  meetingDescription?: string;
  conductPersonId?: number;
  coordinatorId?: number;
  participantIds: number[];
  momRequired?: boolean;
  meetingAttachmentFile?: File | null;
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

export interface UserOption {
  applicantId: number;
  firstName: string;
  lastName: string;
  email: string;
}
