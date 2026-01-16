import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CategoryDto } from '../models/api-models';

@Injectable({
  providedIn: 'root'
})

export class CategoriesService {
  private apiUrl = `${environment.apiUrl}/Categories`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<CategoryDto> {
    let params = new HttpParams().set('id', id);
    return this.http.get<CategoryDto>(`${this.apiUrl}/get`, { params });
  }

  getAll(): Observable<CategoryDto[]> {
    return this.http.get<CategoryDto[]>(`${this.apiUrl}/get-all`);
  }

  add(category: CategoryDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, category);
  }

  update(category: CategoryDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update`, category);
  }

  remove(id: string): Observable<void> {
    let params = new HttpParams().set('id', id);
    return this.http.delete<void>(`${this.apiUrl}/remove`, { params });
  }
}
