import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PackageDto } from '../models/api-models';

@Injectable({
  providedIn: 'root'
})
export class PackagesService {
  private apiUrl = `${environment.apiUrl}/api/packages`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<PackageDto> {
    return this.http.get<PackageDto>(`${this.apiUrl}/${id}`);
  }

  update(id: string, packageDto: PackageDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, packageDto);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getAll(): Observable<PackageDto[]> {
    return this.http.get<PackageDto[]>(`${this.apiUrl}`);
  }

  create(packageDto: PackageDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, packageDto);
  }
}
