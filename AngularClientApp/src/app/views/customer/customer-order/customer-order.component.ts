import { Component, OnInit } from '@angular/core';
import { ICustomer, IOrder, ICustomerOrder } from 'src/app/shared/interfaces';
import { CustomerDataService } from 'src/app/core/services/customer-data.service';
import { CustomerOrderDataService } from 'src/app/core/services/customer-order-data.service';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-customer-order',
  templateUrl: './customer-order.component.html',
  styleUrls: ['./customer-order.component.scss']
})
export class CustomerOrderComponent implements OnInit {

  customerOrder: ICustomerOrder;
  
  constructor(private route: ActivatedRoute, private orderDataService: CustomerOrderDataService) { }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      const customerId = +params['customerId'];
      this.orderDataService.getOrderByCustomerId(customerId).subscribe((customerOrder: ICustomerOrder) => {
        this.customerOrder = customerOrder;
      });
    });
  }

  ordersTrackBy(index: number, orderItem: any) {
    return index;
  }

}
