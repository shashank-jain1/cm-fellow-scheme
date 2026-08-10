const API_BASE = import.meta.env.VITE_API_URL || '/api/v1';

interface ApiResponse<T> {
  data?: T;
  error?: string;
}

interface RequestOptions {
  /**
   * Treat 404 as "no such record" and resolve with undefined instead of throwing.
   * Only set this for genuinely optional lookups (e.g. "has this fellow submitted a
   * self-assessment yet?"). Leaving it off is what makes a wrong URL visible.
   */
  allowNotFound?: boolean;
}

function authHeaders(extra?: Record<string, string>): Record<string, string> {
  const headers: Record<string, string> = { ...extra };
  const token = localStorage.getItem('token');
  if (token) headers['Authorization'] = `Bearer ${token}`;
  return headers;
}

function handleUnauthorized(): never {
  localStorage.removeItem('token');
  localStorage.removeItem('auth_user');
  window.location.href = '/login';
  throw new Error('Session expired. Please login again.');
}

/**
 * Pulls a human-readable message out of an RFC7807 problem+json body when the API
 * sends one, so validation failures surface their actual reason rather than a status code.
 */
async function describeFailure(response: Response, method: string, url: string): Promise<string> {
  const raw = await response.text().catch(() => '');
  if (raw) {
    try {
      const problem = JSON.parse(raw) as {
        detail?: string;
        title?: string;
        errors?: Record<string, string[]> | string[];
      };
      const fieldErrors = problem.errors;
      if (Array.isArray(fieldErrors) && fieldErrors.length > 0) {
        return fieldErrors.join(' ');
      }
      if (fieldErrors && typeof fieldErrors === 'object') {
        const flattened = Object.values(fieldErrors).flat().filter(Boolean);
        if (flattened.length > 0) return flattened.join(' ');
      }
      if (problem.detail) return problem.detail;
      if (problem.title) return problem.title;
    } catch {
      return raw;
    }
  }
  return `API ${method} ${url} failed (${response.status}): ${response.statusText}`;
}

function triggerDownload(blob: Blob, fileName: string): void {
  const blobUrl = window.URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = blobUrl;
  a.download = fileName;
  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
  window.URL.revokeObjectURL(blobUrl);
}

class ApiService {
  private static async request<T>(
    method: string,
    url: string,
    body?: unknown,
    options?: RequestOptions
  ): Promise<ApiResponse<T>> {
    const response = await fetch(`${API_BASE}/${url}`, {
      method,
      headers: authHeaders({ 'Content-Type': 'application/json' }),
      body: body ? JSON.stringify(body) : undefined,
    });

    if (!response.ok) {
      if (response.status === 401) handleUnauthorized();
      if (response.status === 404 && options?.allowNotFound) {
        return { data: undefined };
      }
      throw new Error(await describeFailure(response, method, url));
    }

    const text = await response.text();
    return { data: text ? JSON.parse(text) : undefined };
  }

  static async get<T>(url: string, options?: RequestOptions) {
    return this.request<T>('GET', url, undefined, options);
  }

  /** GET for endpoints where "not found" is a valid, expected answer. */
  static async getOptional<T>(url: string) {
    return this.request<T>('GET', url, undefined, { allowNotFound: true });
  }

  static async post<T>(url: string, body: unknown, options?: RequestOptions) {
    return this.request<T>('POST', url, body, options);
  }

  static async put<T>(url: string, body: unknown, options?: RequestOptions) {
    return this.request<T>('PUT', url, body, options);
  }

  static async delete<T>(url: string, options?: RequestOptions) {
    return this.request<T>('DELETE', url, undefined, options);
  }

  /** Downloads a file from a GET endpoint, carrying the bearer token. */
  static async getBlob(url: string, fileName: string): Promise<void> {
    const response = await fetch(`${API_BASE}/${url}`, { headers: authHeaders() });

    if (!response.ok) {
      if (response.status === 401) handleUnauthorized();
      throw new Error(await describeFailure(response, 'GET', url));
    }

    triggerDownload(await response.blob(), fileName);
  }

  static async postBlob(url: string, body: unknown, fileName: string): Promise<void> {
    const response = await fetch(`${API_BASE}/${url}`, {
      method: 'POST',
      headers: authHeaders({ 'Content-Type': 'application/json' }),
      body: body ? JSON.stringify(body) : undefined,
    });

    if (!response.ok) {
      if (response.status === 401) handleUnauthorized();
      throw new Error(await describeFailure(response, 'POST', url));
    }

    triggerDownload(await response.blob(), fileName);
  }

  static async postFormData<T>(url: string, formData: FormData): Promise<ApiResponse<T>> {
    const response = await fetch(`${API_BASE}/${url}`, {
      method: 'POST',
      headers: authHeaders(),
      body: formData,
    });

    if (!response.ok) {
      if (response.status === 401) handleUnauthorized();
      throw new Error(await describeFailure(response, 'POST', url));
    }

    const text = await response.text();
    return { data: text ? JSON.parse(text) : undefined };
  }
}

export default ApiService;
export type { ApiResponse, RequestOptions };
