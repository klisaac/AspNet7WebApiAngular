import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICustomerOrder } from 'src/app/shared/interfaces';

@Injectable()
export class CustomerOrderDataService {

  constructor(private httpClient: HttpClient) { }

  getOrderByCustomerId(customerId:number): Observable<ICustomerOrder> {
    return this.httpClient.get<ICustomerOrder>(environment.apiUrl + '/order/getByCustomerId/'+ customerId);
  }
  
}
