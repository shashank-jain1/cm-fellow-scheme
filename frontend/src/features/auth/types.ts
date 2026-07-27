export interface AuthUser {
  userAccountId: number;
  username: string;
  role: string;
}

export interface LoginResponse {
  token: string;
  userAccountId: number;
  username: string;
  role: string;
}
