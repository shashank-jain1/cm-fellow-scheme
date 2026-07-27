import ApiService from '../../../services/ApiService';
import type { UserAccountListItem, CreateUserAccountRequest, AssignRoleRequest } from '../types/user-management';

const BASE = 'registrations/../user-accounts';

export const userManagementApi = {
  list: async (role?: string, isActive?: boolean): Promise<UserAccountListItem[]> => {
    const params = new URLSearchParams();
    if (role) params.set('role', role);
    if (isActive !== undefined) params.set('isActive', String(isActive));
    const query = params.toString();
    const url = query ? `${BASE}?${query}` : BASE;
    const res = await ApiService.get<UserAccountListItem[]>(url);
    return res.data ?? [];
  },

  create: async (request: CreateUserAccountRequest): Promise<number> => {
    const res = await ApiService.post<number>(BASE, request);
    return res.data ?? 0;
  },

  assignRole: async (userAccountId: number, request: AssignRoleRequest): Promise<void> => {
    await ApiService.put(`${BASE}/${userAccountId}/assign-role`, request);
  },

  deactivate: async (userAccountId: number): Promise<void> => {
    await ApiService.put(`${BASE}/${userAccountId}/deactivate`, {});
  },
};
