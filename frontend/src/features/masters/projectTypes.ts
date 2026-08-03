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

export interface UpdateWorkCommand {
  workName: string;
  workDescription?: string;
  priority: string;
  startDate: string;
  endDate: string;
  assignedTo: string;
  remarks?: string;
}
