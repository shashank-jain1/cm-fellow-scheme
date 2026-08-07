export interface WorkAllocationFormData {
  projectId: number;
  workProjectId: string;
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
  workProjectId: string;
  workDescription: string;
  priority: string;
  startDate: string;
  endDate: string;
  durationDays: number;
  surveysPerIntern: number;
  divisionId: number;
  districtId: number;
  blockId: number;
  activeStatus: boolean;
  status: string;
  assignedToUserId?: number;
}

export interface TaskProgressDto {
  taskProgressId: number;
  workAllocationId: number;
  projectName: string;
  workProject: string;
  workDescription: string;
  priority: string;
  numberOfSurveys: number;
  completedSurveys: number;
  pendingSurveys: number;
  completionPercentage: number;
  completionDate?: string;
  workStatus: string;
}

export interface SurveyDetailDto {
  surveyRecordId: number;
  taskProgressId: number;
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
  taskDependencyId: number;
  workAllocationId: number;
  dependsOnWorkAllocationId: number;
  createdOn: string;
}

export interface TaskDependencyFormData {
  workAllocationId: number;
  dependsOnWorkAllocationId: number;
}

export interface TaskAttachment {
  taskAttachmentId: number;
  workAllocationId: number;
  userAccountId: number;
  fileName: string;
  filePath: string;
  fileSize: number;
  contentType: string;
  createdOn: string;
}

export interface UpdateProgressCommand {
  progressNotes: string;
  progressPercentage: number;
  status: string;
}

export interface VerifyTaskCommand {
  verificationStatus: 'Approved' | 'Rejected';
  comments?: string;
}

export interface OverdueTaskDto {
  workAllocationId: number;
  workDescription: string;
  endDate: string;
  assignedToUserId?: number;
  daysOverdue: number;
}
