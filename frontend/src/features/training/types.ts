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
  certificateRequired?: boolean;
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

export interface TrainingCompletion {
  completionId: number;
  trainingScheduleId: number;
  trainingTitle: string;
  fellowId: number;
  fellowName: string;
  completedAt: string;
  rating: number;
  comments: string;
  status: string;
}

export interface TrainingCompletionFormData {
  trainingScheduleId: number;
  fellowId: number;
  rating: number;
  comments: string;
}

/** Mirrors TrainingMaterialDto returned by GET training/sessions/{id}/materials. */
export interface TrainingMaterial {
  trainingMaterialId: number;
  trainingScheduleId: number;
  materialName: string;
  filePath: string;
  fileSize: number;
  contentType?: string | null;
  uploadedOn: string;
  isActive: boolean;
}

export interface UserOption {
  applicantId: number;
  firstName: string;
  lastName: string;
  email: string;
}
