export interface WorkAllocationFormData {
  projectId: number;
  workProjectId: number;
  workDescription: string;
  priority: 'High' | 'Medium' | 'Low';
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
}

export interface TaskProgressDto {
  taskProgressId: number;
  projectName: string;
  workProject: string;
  numberOfSurveys: number;
  completedSurveys: number;
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
