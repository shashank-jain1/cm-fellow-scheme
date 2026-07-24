const API_BASE = import.meta.env.VITE_API_URL || 'http://localhost:5000/api/v1';

interface ApiResponse<T> {
  data?: T;
  error?: string;
}

class ApiService {
  private static async request<T>(method: string, url: string, body?: unknown): Promise<ApiResponse<T>> {
    try {
      const headers: Record<string, string> = { 'Content-Type': 'application/json' };
      const token = localStorage.getItem('token');
      if (token) headers['Authorization'] = `Bearer ${token}`;

      const response = await fetch(`${API_BASE}/${url}`, {
        method,
        headers,
        body: body ? JSON.stringify(body) : undefined,
      });

      if (!response.ok) {
        const errorData = await response.json().catch(() => ({ detail: 'Request failed' }));
        return { error: errorData.detail || errorData.title || 'Request failed' };
      }

      const text = await response.text();
      return { data: text ? JSON.parse(text) : undefined };
    } catch (err) {
      return { error: err instanceof Error ? err.message : 'Network error' };
    }
  }

  static async get<T>(url: string) { return this.request<T>('GET', url); }
  static async post<T>(url: string, body: unknown) { return this.request<T>('POST', url, body); }
  static async put<T>(url: string, body: unknown) { return this.request<T>('PUT', url, body); }
  static async delete<T>(url: string) { return this.request<T>('DELETE', url); }
}

export default ApiService;
