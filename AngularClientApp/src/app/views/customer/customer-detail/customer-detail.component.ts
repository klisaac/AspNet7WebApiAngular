import { Component, OnInit } from '@angular/core';
import { ICustomer } from 'src/app/shared/interfaces';
import { ActivatedRoute, Params } from '@angular/router';
import { CustomerDataService } from 'src/app/core/services/customer-data.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})
export class CustomerDetailComponent implements OnInit {

  customer: ICustomer

  constructor(private route: ActivatedRoute, private dataService: CustomerDataService) { }

  ngOnInit() {

    this.route.params.subscribe((params: Params) => {
      const customerId = +params['customerId'];
      if (customerId) {
        this.dataService.getCustomerById(customerId)
          .subscribe((customer: ICustomer) => {
            this.customer = customer;
          });
      }
    });

  }

}
