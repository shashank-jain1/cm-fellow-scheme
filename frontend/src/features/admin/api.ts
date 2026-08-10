import ApiService from '../../services/ApiService';
import { USER_MODULE_ACCESS_URL, MODULE_MASTER_URL } from './urls';
import type {
  AuditLogEntry,
  ModuleAccessDto,
  UserAccessSummaryDto,
  ModuleMasterDto,
  BulkUpdateRequest,
  GrantAccessRequest,
} from './types';

export const moduleAccessApi = {
  fetchAllModuleAccess: () =>
    ApiService.get<UserAccessSummaryDto[]>(USER_MODULE_ACCESS_URL),

  fetchUserModuleAccess: (userAccountId: number) =>
    ApiService.get<ModuleAccessDto[]>(`${USER_MODULE_ACCESS_URL}/${userAccountId}`),

  bulkUpdateModuleAccess: (data: BulkUpdateRequest) =>
    ApiService.put<void>(`${USER_MODULE_ACCESS_URL}/bulk`, data),

  grantModuleAccess: (data: GrantAccessRequest) =>
    ApiService.post<void>(`${USER_MODULE_ACCESS_URL}/grant`, data),

  revokeModuleAccess: (id: number) =>
    ApiService.delete<void>(`${USER_MODULE_ACCESS_URL}/${id}`),

  fetchModules: () =>
    ApiService.get<ModuleMasterDto[]>(MODULE_MASTER_URL),

  fetchAuditLogs: (params?: { userAccountId?: number; moduleMasterId?: number; pageSize?: number; pageNumber?: number }) => {
    const queryString = new URLSearchParams();
    if (params?.userAccountId) queryString.set('userAccountId', String(params.userAccountId));
    if (params?.moduleMasterId) queryString.set('moduleMasterId', String(params.moduleMasterId));
    if (params?.pageSize) queryString.set('pageSize', String(params.pageSize));
    if (params?.pageNumber) queryString.set('pageNumber', String(params.pageNumber));
    const qs = queryString.toString();
    return ApiService.get<AuditLogEntry[]>(`${USER_MODULE_ACCESS_URL}/audit-logs${qs ? '?' + qs : ''}`);
  },
};
