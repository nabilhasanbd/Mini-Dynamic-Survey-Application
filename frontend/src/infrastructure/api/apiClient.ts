/**
 * A simple fetch wrapper to handle API requests.
 */
const BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api';

export const apiClient = {
  get: async <T>(endpoint: string): Promise<T> => {
    const res = await fetch(`${BASE_URL}${endpoint}`, {
      headers: {
        'Content-Type': 'application/json',
        // 'Authorization': `Bearer ${token}` // TODO: Add auth state
      },
    });
    if (!res.ok) throw new Error(await res.text());
    return res.json();
  },

  post: async <T>(endpoint: string, body: any): Promise<T> => {
    const res = await fetch(`${BASE_URL}${endpoint}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body),
    });
    if (!res.ok) throw new Error(await res.text());
    return res.json();
  },

  // Implement PUT, PATCH, DELETE similarly...
};
