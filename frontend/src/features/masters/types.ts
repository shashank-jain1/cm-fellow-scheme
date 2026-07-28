export interface StateDto {
  stateId: number;
  stateName: string;
  stateCode: string;
  stateShortName: string | null;
  displayOrder: number | null;
  isActive: boolean;
}

export interface DivisionDto {
  divisionId: number;
  stateId: number;
  divisionName: string;
  divisionCode: string | null;
  isActive: boolean;
}

export interface DistrictDto {
  districtId: number;
  divisionId: number;
  districtName: string;
  districtCode: string | null;
  isActive: boolean;
}

export interface BlockDto {
  blockId: number;
  districtId: number;
  blockName: string;
  blockCode: string | null;
  isActive: boolean;
}

export interface GramPanchayatDto {
  gramPanchayatId: number;
  blockId: number;
  gramPanchayatName: string;
  gpCode: string | null;
  isActive: boolean;
}

export interface ProjectDto {
  projectId: number;
  projectName: string;
  projectCode: string;
  projectDescription: string | null;
  departmentName: string;
  startDate: string;
  endDate: string;
  projectIncharge: string;
  budgetAmount: number | null;
  isActive: boolean;
}

export interface WorkDto {
  workId: number;
  projectId: number;
  workName: string;
  workDescription: string | null;
  priority: string;
  startDate: string;
  endDate: string;
  assignedTo: string;
  remarks: string | null;
  isActive: boolean;
}

export interface TrainingScheduleDto {
  trainingScheduleId: number;
  calendarYear: string;
  projectId: number;
  projectName: string;
  workId: number | null;
  workName: string | null;
  divisionId: number | null;
  divisionName: string | null;
  districtId: number | null;
  districtName: string | null;
  blockId: number | null;
  blockName: string | null;
  trainingDate: string;
  venueName: string | null;
  trainingDescription: string | null;
  isActive: boolean;
}

export interface CreateStateCommand {
  stateName: string;
  stateCode: string;
  stateShortName?: string;
  displayOrder?: number;
}

export interface CreateDivisionCommand {
  stateId: number;
  divisionName: string;
  divisionCode?: string;
}

export interface CreateDistrictCommand {
  divisionId: number;
  districtName: string;
  districtCode?: string;
}

export interface CreateBlockCommand {
  districtId: number;
  blockName: string;
  blockCode?: string;
}

export interface CreateGramPanchayatCommand {
  blockId: number;
  gramPanchayatName: string;
  gpCode?: string;
}

export interface CreateProjectCommand {
  projectName: string;
  projectCode: string;
  projectDescription?: string;
  departmentName: string;
  startDate: string;
  endDate: string;
  projectIncharge: string;
  budgetAmount?: number;
}

export interface CreateWorkCommand {
  projectId: number;
  workName: string;
  workDescription?: string;
  priority: string;
  startDate: string;
  endDate: string;
  assignedTo: string;
  remarks?: string;
}

export interface CreateTrainingScheduleCommand {
  calendarYear: string;
  projectId: number;
  workId?: number;
  divisionId?: number;
  districtId?: number;
  blockId?: number;
  trainingDate: string;
  venueName?: string;
  trainingDescription?: string;
}

export interface UpdateTrainingScheduleCommand {
  trainingScheduleId: number;
  calendarYear: string;
  projectId: number;
  workId?: number;
  divisionId?: number;
  districtId?: number;
  blockId?: number;
  trainingDate: string;
  venueName?: string;
  trainingDescription?: string;
}
