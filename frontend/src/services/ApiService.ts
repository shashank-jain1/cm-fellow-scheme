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
      if (response.status === 401) {
        localStorage.removeItem('token');
        localStorage.removeItem('auth_user');
        window.location.href = '/login';
        throw new Error('Session expired. Please login again.');
      }
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

  static async postBlob(url: string, body: unknown, fileName: string): Promise<void> {
    const headers: Record<string, string> = {};
    const token = localStorage.getItem('token');
    if (token) headers['Authorization'] = `Bearer ${token}`;

    const response = await fetch(`${API_BASE}/${url}`, {
      method: 'POST',
      headers,
      body: body ? JSON.stringify(body) : undefined,
    });

    if (!response.ok) {
      if (response.status === 401) {
        localStorage.removeItem('token');
        localStorage.removeItem('auth_user');
        window.location.href = '/login';
        throw new Error('Session expired. Please login again.');
      }
      const errorText = await response.text().catch(() => response.statusText);
      throw new Error(`API POST ${url} failed (${response.status}): ${errorText}`);
    }

    const blob = await response.blob();
    const blobUrl = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = blobUrl;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    window.URL.revokeObjectURL(blobUrl);
  }

  static async postFormData<T>(url: string, formData: FormData): Promise<ApiResponse<T>> {
    const headers: Record<string, string> = {};
    const token = localStorage.getItem('token');
    if (token) headers['Authorization'] = `Bearer ${token}`;

    const response = await fetch(`${API_BASE}/${url}`, {
      method: 'POST',
      headers,
      body: formData,
    });

    if (!response.ok) {
      if (response.status === 401) {
        localStorage.removeItem('token');
        localStorage.removeItem('auth_user');
        window.location.href = '/login';
        throw new Error('Session expired. Please login again.');
      }
      const errorText = await response.text().catch(() => response.statusText);
      throw new Error(`API POST ${url} failed (${response.status}): ${errorText}`);
    }

    const text = await response.text();
    return { data: text ? JSON.parse(text) : undefined };
  }
}

export default ApiService;
