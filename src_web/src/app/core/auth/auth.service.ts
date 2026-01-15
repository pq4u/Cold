import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginRequestDto, AuthResponseDto, RegisterRequestDto } from '../models/api-models';
import { environment } from '../../../environments/environment';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient) {}

  login(credentials: LoginRequestDto): Observable<AuthResponseDto> {
    return this.http.post<AuthResponseDto>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        if (response.token) {
          localStorage.setItem('token', response.token);
        }
        if (response.userId) {
          localStorage.setItem('userId', response.userId);
        }
      })
    );
  }

  register(data: RegisterRequestDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/register`, data);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      // Claims can vary. Common standard for role is:
      // "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      // or simple "role"
      return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || decoded['role'] || null;
    } catch (e) {
      return null;
    }
  }

  isAdmin(): boolean {
    const role = this.getRole();
    return role === 'Administrator' || role === 'Admin';
  }
}