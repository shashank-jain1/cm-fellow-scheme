export interface ModulePermissions {
  canRead: boolean;
  canWrite: boolean;
  canApprove: boolean;
  canExport: boolean;
}

export interface AuthUser {
  userAccountId: number;
  username: string;
  role: string;
  modules?: Record<string, ModulePermissions>;
}

export interface LoginResponse {
  token: string;
  userAccountId: number;
  username: string;
  role: string;
  modules?: Record<string, ModulePermissions>;
}
