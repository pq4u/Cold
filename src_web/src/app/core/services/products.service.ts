import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductDto } from '../models/api-models';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private apiUrl = `${environment.apiUrl}/Products`;

  constructor(private http: HttpClient) { }

  get(id: string): Observable<ProductDto> {
    let params = new HttpParams().set('id', id);
    return this.http.get<ProductDto>(`${this.apiUrl}/get`, { params });
  }

  getAll(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${this.apiUrl}/get-all`);
  }

  add(product: ProductDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, product);
  }

  update(product: ProductDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update`, product);
  }

  remove(id: string): Observable<void> {
    let params = new HttpParams().set('id', id);
    return this.http.delete<void>(`${this.apiUrl}/remove`, { params });
  }
}
