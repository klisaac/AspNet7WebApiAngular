import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICustomer } from 'src/app/shared/interfaces';
import { map } from 'rxjs/operators';

@Injectable()
export class CustomerDataService {
  
    constructor(private httpClient: HttpClient) { }

    getCustomers(): Observable<ICustomer[]> {
        return this.httpClient.get<ICustomer[]>(environment.apiUrl + '/customer/getAll');
    }

    getCustomerById(customerId:number): Observable<ICustomer> {
        //var request = { customerId: customerId };
        //return this.httpClient.post<ICustomer>(environment.apiUrl + '/customer/getByCustomerId/', request);
        return this.httpClient.get<ICustomer>(environment.apiUrl + '/customer/getById/'+ customerId);        
    }  

    deleteCustomer(customerId: number) : Observable<boolean> {
        return this.httpClient.delete<boolean>(environment.apiUrl + '/customer/delete/'+ customerId);
            // .pipe(
            //     map(res => true)
            // );
    }

    updateCustomer(customerId: number): Observable<ICustomer> {
        return this.httpClient.put<ICustomer>(environment.apiUrl + '/customer/update', customerId);
        // .pipe(
        //     map(res => true)
        // );
    }
}
