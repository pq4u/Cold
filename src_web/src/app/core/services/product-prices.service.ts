import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductPriceDto } from '../models/api-models';

@Injectable({
  providedIn: 'root'
})
export class ProductPricesService {
  private apiUrl = `${environment.apiUrl}/ProductPrices`;

  constructor(private http: HttpClient) { }

  get(productId: string): Observable<ProductPriceDto[]> {
    let params = new HttpParams().set('productId', productId);
    return this.http.get<ProductPriceDto[]>(`${this.apiUrl}/get`, { params });
  }

  getAll(): Observable<ProductPriceDto[]> {
    return this.http.get<ProductPriceDto[]>(`${this.apiUrl}/get-all`);
  }

  add(price: ProductPriceDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, price);
  }

  update(price: ProductPriceDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update`, price);
  }
}
