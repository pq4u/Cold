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

  getUserId(): string | null {
    return localStorage.getItem('userId');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);
      return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || decoded['role'] || null;
    } catch (e) {
      return null;
    }
  }

  hasRole(allowedRoles: string[]): boolean {
    const role = this.getRole();
    if (!role) return false;
    return allowedRoles.includes(role);
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin' || this.getRole() === 'Administrator';
  }

  isEmployee(): boolean {
    return this.getRole() === 'Employee';
  }

  isSupplier(): boolean {
    return this.getRole() === 'Supplier';
  }
}