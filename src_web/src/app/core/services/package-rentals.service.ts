import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PackageRentalDto, CreatePackageRentalRequestDto } from '../models/api-models';

@Injectable({
  providedIn: 'root'
})
export class PackageRentalsService {
  private apiUrl = `${environment.apiUrl}/api/package-rentals`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<PackageRentalDto> {
    return this.http.get<PackageRentalDto>(`${this.apiUrl}/${id}`);
  }

  getRequested(): Observable<PackageRentalDto[]> {
    return this.http.get<PackageRentalDto[]>(`${this.apiUrl}/requested`);
  }

  getActive(): Observable<PackageRentalDto[]> {
    return this.http.get<PackageRentalDto[]>(`${this.apiUrl}/active`);
  }

  request(requestDto: CreatePackageRentalRequestDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/request`, requestDto);
  }

  approve(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/approve`, {});
  }

  reject(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/reject`, {});
  }

  return(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/return`, {});
  }
}
