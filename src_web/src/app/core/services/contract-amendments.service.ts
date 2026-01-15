import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ContractAmendmentDto } from '../models/api-models';

@Injectable({
  providedIn: 'root'
})
export class ContractAmendmentsService {
  private apiUrl = `${environment.apiUrl}/ContractAmendments`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<ContractAmendmentDto> {
    let params = new HttpParams().set('id', id);
    return this.http.get<ContractAmendmentDto>(`${this.apiUrl}/get`, { params });
  }

  getByContract(contractId: string): Observable<ContractAmendmentDto[]> {
    let params = new HttpParams().set('contractId', contractId);
    return this.http.get<ContractAmendmentDto[]>(`${this.apiUrl}/get-by-contract`, { params });
  }

  getAll(): Observable<ContractAmendmentDto[]> {
    return this.http.get<ContractAmendmentDto[]>(`${this.apiUrl}/get-all`);
  }

  add(amendment: ContractAmendmentDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, amendment);
  }
}
