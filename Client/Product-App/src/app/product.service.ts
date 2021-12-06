import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  readonly ApiURL = 'http://localhost:16067/api'
  constructor(private http: HttpClient) { }
  
  
  public getProduct(){
    return this.http.get(this.ApiURL+'/Product');
  }

  public getProductById(id: string){
    const params = new HttpParams().set('id', id);
    return this.http.get(this.ApiURL+'/Product/GetProductDetail', { params: params });
  }
}
