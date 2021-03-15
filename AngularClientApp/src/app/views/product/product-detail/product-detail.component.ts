import { Component, OnInit } from '@angular/core';

import { IProduct } from 'src/app/shared/interfaces';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductDataService } from 'src/app/core/services/product-data.service';

@Component({
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  product: IProduct = {
    productId: null,
    code:'',
    name: '',
    description: '',
    unitPrice: 0,
    category: { categoryId: null }
  };

  constructor(private dataService: ProductDataService, private router: Router, private route: ActivatedRoute) {
    route.params.subscribe(val => {
      const productId = +this.route.snapshot.paramMap.get('productId');

      if (productId !== undefined && productId != null && productId !== 0) {
        this.dataService.getProductById(productId).subscribe((product: IProduct) => {
          if (product != null) {
            this.product = product;
          }
          else {
            this.router.navigate(['/']);
          }
        });
      }
      else {
        this.router.navigate(['/']);
      }
    });
  }

  ngOnInit() {
  }

  close() {
    this.router.navigate(['/product/product-list']);
  }

  update() {
    this.router.navigate(['/product/product-edit/' + this.product.productId]);
  }
}