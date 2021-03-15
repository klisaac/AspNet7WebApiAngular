import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgForm } from '@angular/forms';

import { ICustomer } from 'src/app/shared/interfaces';
import { CustomerDataService } from 'src/app/core/services/customer-data.service';
import { LoggerService } from 'src/app/core/services/logger.service';

@Component({  
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.scss']
})

export class CustomerEditComponent implements OnInit {

  customer: ICustomer = {
      customerId: 0,
      name: '',
      surName: '',
      address: '',
      city: '',
      state: '',
      email:'',
      citizenId:''
  };
  errorMessage: string;
  deleteMessageEnabled: boolean;
  operationText = 'Insert';
  @ViewChild('customerForm', { static : true }) customerForm: NgForm;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private dataService: CustomerDataService,
    private logger: LoggerService) { }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      const customerId = +params['customerId'];
      if (customerId !== 0) {
        this.operationText = 'Update';
        this.getCustomer(customerId);
      }
    });
  }

  getCustomer(customerId: number) {
    this.dataService.getCustomerById(customerId).subscribe((customer : ICustomer) => { 
      this.customer = customer 
    });
  }

  cancel(event: Event) {
    event.preventDefault();
    this.router.navigate(['/customer']);
  }

  delete(event: Event) {
    event.preventDefault();

    this.dataService.deleteCustomer(this.customer.customerId)
      .subscribe((status: boolean) => {
        if (status) {
          this.router.navigate(['/customer']);
        } else {
          this.errorMessage = 'Unable to delete customer';
        }
      },
        (err) => this.logger.log(err));
  }

  submit() {
    if (this.customer.customerId === 0) {
      this.router.navigate(['/customer']);
    } else {
      this.router.navigate(['/customer']);
    }
  }

}