import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateDeliveryDto, DeliveryDto } from '../models/api-models';

@Injectable({
  providedIn: 'root'
})
export class DeliveriesService {
  private apiUrl = `${environment.apiUrl}/Deliveries`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<DeliveryDto> {
    return this.http.get<DeliveryDto>(`${this.apiUrl}/${id}`);
  }

  update(id: string, delivery: CreateDeliveryDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, delivery);
  }

  getByNumber(deliveryNumber: string): Observable<DeliveryDto> {
    return this.http.get<DeliveryDto>(`${this.apiUrl}/by-number/${deliveryNumber}`);
  }

  getAll(): Observable<DeliveryDto[]> {
    return this.http.get<DeliveryDto[]>(`${this.apiUrl}`);
  }

  create(delivery: CreateDeliveryDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, delivery);
  }

  getBySupplier(supplierId: string): Observable<DeliveryDto[]> {
    return this.http.get<DeliveryDto[]>(`${this.apiUrl}/by-supplier/${supplierId}`);
  }

  getUninvoiced(): Observable<DeliveryDto[]> {
    return this.http.get<DeliveryDto[]>(`${this.apiUrl}/uninvoiced`);
  }

  getUninvoicedBySupplier(supplierId: string): Observable<DeliveryDto[]> {
    return this.http.get<DeliveryDto[]>(`${this.apiUrl}/uninvoiced/by-supplier/${supplierId}`);
  }

  markInvoiced(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/mark-invoiced`, {});
  }
}
