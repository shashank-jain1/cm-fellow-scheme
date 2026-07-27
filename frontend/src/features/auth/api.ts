import ApiService from '../../services/ApiService';
import type { LoginResponse } from './types';

export async function loginApi(username: string, password: string): Promise<LoginResponse> {
  const res = await ApiService.post<LoginResponse>('auth/login', { username, password });
  if (!res.data) {
    throw new Error('Login failed');
  }
  return res.data;
}
