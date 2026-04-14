import { Injectable, signal, computed, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthResponse, LoginRequest, RegisterRequest } from '../models/auth.model';

const TOKEN_KEY = 'auth_token';
const USER_KEY  = 'auth_user';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http   = inject(HttpClient);
  private router = inject(Router);

  currentUser = signal<AuthResponse | null>(null);
  isLoggedIn  = computed(() => !!this.currentUser());

  loadUser(): void {
    const stored = localStorage.getItem(USER_KEY);
    if (stored) this.currentUser.set(JSON.parse(stored));
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  login(request: LoginRequest) {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/login`, request).pipe(
      tap(res => {
        localStorage.setItem(TOKEN_KEY, res.token);
        localStorage.setItem(USER_KEY, JSON.stringify(res));
        this.currentUser.set(res);
      })
    );
  }

  register(request: RegisterRequest) {
    return this.http.post<AuthResponse>(`${environment.apiUrl}/auth/register`, request);
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }
}
