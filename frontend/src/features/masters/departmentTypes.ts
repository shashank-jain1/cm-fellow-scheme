export interface DepartmentDto {
  departmentId: number;
  departmentName: string;
  departmentCode: string;
  isActive: boolean;
}

export interface CreateDepartmentCommand {
  departmentName: string;
  departmentCode: string;
}
