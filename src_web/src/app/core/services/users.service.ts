import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface UserDto {
  id: string;
  email: string;
  roles: string[];
}

@Injectable({
  providedIn: 'root'
})


export class UsersService {
  private apiUrl = `${environment.apiUrl}/users`;
  constructor(private http: HttpClient) { }

  getSuppliers(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(`${this.apiUrl}/suppliers`);
  }

  getAll(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(this.apiUrl);
  }

  updateRole(userId: string, role: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${userId}/role`, { role });
  }
}
