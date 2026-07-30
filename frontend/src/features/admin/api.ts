import ApiService from '../../services/ApiService';
import { USER_MODULE_ACCESS_URL, MODULE_MASTER_URL } from './urls';
import type {
  ModuleAccessDto,
  UserAccessSummaryDto,
  ModuleMasterDto,
  BulkUpdateRequest,
  GrantAccessRequest,
} from './types';

export const moduleAccessApi = {
  fetchAllModuleAccess: () =>
    ApiService.get<ModuleAccessDto[]>(USER_MODULE_ACCESS_URL),

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
};
