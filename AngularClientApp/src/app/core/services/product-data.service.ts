import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IProduct, IPagedList, IProductCreate, IProductEdit } from 'src/app/shared/interfaces';

@Injectable()
export class ProductDataService {

  constructor(private httpClient: HttpClient) { }

  getProducts(): Observable<IProduct[]> {
      return this.httpClient.get<IProduct[]>(environment.apiUrl + '/product/getAll');
  }

  searchProducts(args: any): Observable<IPagedList<IProduct>> {
      var request = { args: args };

      return this.httpClient.post<IPagedList<IProduct>>(environment.apiUrl + '/product/search', request);
  }

  getProductById(productId: number): Observable<IProduct> {
      return this.httpClient.get<IProduct>(environment.apiUrl + '/product/getById/' + productId);
  }

  getProductsByCode(code: string): Observable<IProduct[]> {
      return this.httpClient.get<IProduct[]>(environment.apiUrl + '/product/getByCode/' + code);
  }

  getProductsByCategoryId(categoryId: string): Observable<IProduct[]> {
      return this.httpClient.get<IProduct[]>(environment.apiUrl + '/product/getByCategoryId/' + categoryId);
  }

  createProduct(product: IProductCreate): Observable<IProduct> {
      return this.httpClient.post<IProduct>(environment.apiUrl + '/product/create', product);
  }

  updateProduct(product: IProductEdit): Observable<IProduct> {
      return this.httpClient.put<IProduct>(environment.apiUrl + '/product/update', product);
  }

  deleteProduct(productId: number): Observable<boolean> {
      return this.httpClient.delete<boolean>(environment.apiUrl + '/product/delete/'+ productId);
  }
}
