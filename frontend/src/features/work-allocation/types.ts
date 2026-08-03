export interface WorkAllocationFormData {
  projectId: number;
  workProjectId: number;
  workDescription: string;
  priority: string;
  startDate: string;
  endDate: string;
  surveysPerIntern: number;
  divisionId: number;
  districtId: number;
  blockId: number;
}

export interface WorkAllocationDto {
  workAllocationId: number;
  projectId: number;
  workProjectId: number;
  workDescription: string;
  priority: string;
  startDate: string;
  endDate: string;
  durationDays: number;
  surveysPerIntern: number;
  status: string;
  completionPercentage: number;
  assignedToUserId?: number;
}

export interface TaskProgressDto {
  taskProgressId: number;
  projectName: string;
  workProject: string;
  numberOfSurveys: number;
  completedSurveys: number;
  pendingSurveys: number;
  completionPercentage: number;
  workStatus: string;
}

export interface SurveyDetailDto {
  surveyRecordId: number;
  internName: string;
  surveyPersonName: string;
  mobileNumber: string;
  panchayatName: string;
  villageName: string;
  surveyDate: string;
  surveyStatus: string;
  latitude: number;
  longitude: number;
}

export interface AssignWorkAllocationCommand {
  workAllocationId: number;
  assignedToUserId: number;
}

export interface TaskDependency {
  dependencyId: number;
  taskProgressId: number;
  prerequisiteTaskProgressId: number;
  prerequisiteTaskName: string;
  createdAt: string;
}

export interface TaskDependencyFormData {
  taskProgressId: number;
  prerequisiteTaskProgressId: number;
}

export interface TaskAttachment {
  attachmentId: number;
  taskProgressId: number;
  fileName: string;
  fileUrl: string;
  fileSize: number;
  uploadedBy: number;
  uploadedByName: string;
  uploadedAt: string;
}

export interface UpdateProgressCommand {
  completedSurveys: number;
  status: string;
  remarks?: string;
}

export interface VerifyTaskCommand {
  status: 'Approved' | 'Rejected';
  comments: string;
}

export interface OverdueTaskDto {
  workAllocationId: number;
  workDescription: string;
  endDate: string;
  assignedToUserId?: number;
  daysOverdue: number;
}
