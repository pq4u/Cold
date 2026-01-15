// import { Injectable } from '@angular/core';
// import { HttpClient, HttpParams } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { environment } from '../../../environments/environment';
// import { ContractDto } from '../models/api-models';

// @Injectable({
//   providedIn: 'root'
// })
// export class ContractsService {
//   private apiUrl = `${environment.apiUrl}/Contracts`;

//   constructor(private http: HttpClient) { }

//   get(id: string): Observable<ContractDto> {
//     let params = new HttpParams().set('id', id);
//     return this.http.get<ContractDto>(`${this.apiUrl}/get`, { params });
//   }

//   getAll(): Observable<ContractDto[]> {
//     return this.http.get<ContractDto[]>(`${this.apiUrl}/get-all`);
//   }

//   add(contract: ContractDto): Observable<void> {
//     return this.http.post<void>(`${this.apiUrl}/add`, contract);
//   }

//   update(contract: ContractDto): Observable<void> {
//     return this.http.put<void>(`${this.apiUrl}/update`, contract);
//   }

//   generatePdf(contractId: string): Observable<void> {
//     let params = new HttpParams().set('contractId', contractId);
//     return this.http.get<void>(`${this.apiUrl}/generate-pdf`, { params });
//   }
// }
