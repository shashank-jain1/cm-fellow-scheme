export interface ModuleAccessDto {
  userModuleAccessId: number;
  userAccountId: number;
  username: string;
  fullName: string;
  moduleMasterId: number;
  moduleCode: string;
  moduleName: string;
  canRead: boolean;
  canWrite: boolean;
  canApprove: boolean;
  canExport: boolean;
  divisionId?: number;
  districtId?: number;
  blockId?: number;
  isActive: boolean;
}

export interface UserAccessSummaryDto {
  userAccountId: number;
  username: string;
  fullName: string;
  role: string;
  divisionId?: number;
  divisionName?: string;
  moduleAccesses: ModuleAccessDto[];
}

export interface ModuleMasterDto {
  moduleMasterId: number;
  moduleCode: string;
  moduleName: string;
  description?: string;
  sortOrder: number;
}

export interface BulkUpdateRequest {
  userAccountId: number;
  performedBy: number;
  accesses: ModuleAccessItem[];
}

export interface ModuleAccessItem {
  moduleMasterId: number;
  canRead: boolean;
  canWrite: boolean;
  canApprove: boolean;
  canExport: boolean;
  divisionId?: number;
  districtId?: number;
  blockId?: number;
}

export interface GrantAccessRequest {
  userAccountId: number;
  moduleMasterId: number;
  performedBy: number;
  canRead: boolean;
  canWrite: boolean;
  canApprove: boolean;
  canExport: boolean;
  divisionId?: number;
  districtId?: number;
  blockId?: number;
}

export interface AuditLogEntry {
  moduleAccessAuditLogId: number;
  userModuleAccessId: number;
  userAccountId: number;
  username: string;
  fullName: string;
  moduleMasterId: number;
  moduleCode: string;
  moduleName: string;
  action: string;
  oldValues?: string;
  newValues?: string;
  performedBy: number;
  performerName: string;
  performedOn: string;
  reason?: string;
}
