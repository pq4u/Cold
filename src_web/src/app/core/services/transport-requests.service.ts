import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { 
  TransportRequestDto, 
  CreateTransportRequestDto, 
  TransportStatusDto, 
  UpdateTransportStatusDto, 
  LinkTransportRequestToDeliveryDto 
} from '../models/api-models';

@Injectable({
  providedIn: 'root'
})
export class TransportRequestsService {
  private apiUrl = `${environment.apiUrl}/TransportRequests`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<TransportRequestDto> {
    return this.http.get<TransportRequestDto>(`${this.apiUrl}/${id}`);
  }

  updateDetails(id: string, scheduledPickupDate: string, notes: string): Observable<void> {
    let params = new HttpParams();
    if (scheduledPickupDate) params = params.set('scheduledPickupDate', scheduledPickupDate);
    if (notes) params = params.set('notes', notes);
    
    return this.http.patch<void>(`${this.apiUrl}/${id}`, {}, { params });
  }

  getAll(): Observable<TransportRequestDto[]> {
    return this.http.get<TransportRequestDto[]>(`${this.apiUrl}`);
  }

  create(request: CreateTransportRequestDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, request);
  }

  getBySupplier(supplierId: string): Observable<TransportRequestDto[]> {
    return this.http.get<TransportRequestDto[]>(`${this.apiUrl}/by-supplier/${supplierId}`);
  }

  getByStatus(statusId: number): Observable<TransportRequestDto[]> {
    return this.http.get<TransportRequestDto[]>(`${this.apiUrl}/by-status/${statusId}`);
  }

  getStatuses(): Observable<TransportStatusDto[]> {
    return this.http.get<TransportStatusDto[]>(`${this.apiUrl}/statuses`);
  }

  updateStatus(id: string, status: UpdateTransportStatusDto): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/status`, status);
  }

  linkDelivery(id: string, linkDto: LinkTransportRequestToDeliveryDto): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/link-delivery`, linkDto);
  }
}
