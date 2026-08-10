import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { userManagementApi } from '../api/user-management';
import type { CreateUserAccountRequest, AssignRoleRequest } from '../types/user-management';

export function useListUserAccounts(role?: string, isActive?: boolean) {
  return useQuery({
    queryKey: ['user-accounts', role, isActive],
    queryFn: () => userManagementApi.list(role, isActive),
  });
}

export function useCreateUserAccount() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (request: CreateUserAccountRequest) => userManagementApi.create(request),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['user-accounts'] });
    },
  });
}

export function useAssignRole() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ userAccountId, request }: { userAccountId: number; request: AssignRoleRequest }) =>
      userManagementApi.assignRole(userAccountId, request),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['user-accounts'] });
    },
  });
}

export function useDeactivateUserAccount() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (userAccountId: number) => userManagementApi.deactivate(userAccountId),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['user-accounts'] });
    },
  });
}

export function useResetPassword() {
  return useMutation({
    mutationFn: ({ userAccountId, newPassword }: { userAccountId: number; newPassword: string }) =>
      userManagementApi.resetPassword(userAccountId, newPassword),
  });
}
