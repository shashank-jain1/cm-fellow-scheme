import { Navigate } from 'react-router-dom';
import type { ReactNode } from 'react';
import { useAuth } from './useAuth';

interface ModuleProtectedRouteProps {
  moduleCode: string;
  permission?: 'Read' | 'Write' | 'Approve' | 'Export';
  children: ReactNode;
}

export function ModuleProtectedRoute({
  moduleCode,
  permission = 'Read',
  children,
}: ModuleProtectedRouteProps) {
  const { user } = useAuth();

  if (user?.role === 'Admin') {
    return <>{children}</>;
  }

  const moduleAccess = user?.modules?.[moduleCode];
  const hasPermission = moduleAccess != null && (
    (permission === 'Read' && moduleAccess.canRead) ||
    (permission === 'Write' && moduleAccess.canWrite) ||
    (permission === 'Approve' && moduleAccess.canApprove) ||
    (permission === 'Export' && moduleAccess.canExport)
  );

  if (!hasPermission) {
    return <Navigate to="/unauthorized" replace />;
  }

  return <>{children}</>;
}
