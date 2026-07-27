const API_BASE = import.meta.env.VITE_API_URL || '/api/v1';

interface ApiResponse<T> {
  data?: T;
  error?: string;
}

class ApiService {
  private static async request<T>(method: string, url: string, body?: unknown): Promise<ApiResponse<T>> {
    const headers: Record<string, string> = { 'Content-Type': 'application/json' };
    const token = localStorage.getItem('token');
    if (token) headers['Authorization'] = `Bearer ${token}`;

    const response = await fetch(`${API_BASE}/${url}`, {
      method,
      headers,
      body: body ? JSON.stringify(body) : undefined,
    });

    if (!response.ok) {
      const errorText = await response.text().catch(() => response.statusText);
      throw new Error(`API ${method} ${url} failed (${response.status}): ${errorText}`);
    }

    const text = await response.text();
    return { data: text ? JSON.parse(text) : undefined };
  }

  static async get<T>(url: string) { return this.request<T>('GET', url); }
  static async post<T>(url: string, body: unknown) { return this.request<T>('POST', url, body); }
  static async put<T>(url: string, body: unknown) { return this.request<T>('PUT', url, body); }
  static async delete<T>(url: string) { return this.request<T>('DELETE', url); }
}

export default ApiService;
