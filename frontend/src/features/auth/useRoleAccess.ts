import { useAuth } from './useAuth';

export interface RoleAccess {
  canRead: boolean;
  canWrite: boolean;
  canApprove: boolean;
  canExport: boolean;
}

export function useRoleAccess(moduleCode: string): RoleAccess {
  const { user } = useAuth();

  if (user?.role === 'Admin') {
    return { canRead: true, canWrite: true, canApprove: true, canExport: true };
  }

  const access = user?.modules?.[moduleCode];
  if (!access) {
    return { canRead: false, canWrite: false, canApprove: false, canExport: false };
  }

  return {
    canRead: access.canRead,
    canWrite: access.canWrite,
    canApprove: access.canApprove,
    canExport: access.canExport,
  };
}
