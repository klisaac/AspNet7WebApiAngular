import { Component, OnInit } from '@angular/core';
import { ICategory, IProduct, IProductEdit, IProductCreate } from 'src/app/shared/interfaces';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProductDataService } from 'src/app/core/services/product-data.service';
import { CategoryDataService } from 'src/app/core/services/category-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, pipe } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {

  categories: ICategory[] = [];
  productForm: FormGroup;
  operationText: string = "Create";

  constructor(
    private productDataService: ProductDataService,
    private categoryDataService: CategoryDataService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute) {
    route.params.subscribe(val => {
      this.initializeForm();

      const productId = +this.route.snapshot.paramMap.get('productId');
      this.categoryDataService.getCategories().subscribe((categories: ICategory[]) => {this.categories = categories});
      if (productId !== undefined && productId != null && productId !== 0) {

        this.productDataService.getProductById(productId).subscribe((product: IProduct) => {
          if (product != null) {
            this.productForm.patchValue(product);
            this.productForm.get('selectorCategory').patchValue(product.category.categoryId);
            this.operationText = 'Update';
          }
          else {
            this.router.navigate(['/product/product-edit']);
          }
        });
      }
    });
  }

  ngOnInit() {
  }

  initializeForm() {
    this.categories = [];
    this.productForm = this.formBuilder.group({
      productId: [0],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      code: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      unitPrice: [0, [Validators.required]],
      selectorCategory: [1, Validators.required]
    });
  }

  saveProduct(product: IProduct) {
    var selectedCategoryId = this.productForm.get('selectorCategory').value;
    
    if (product.productId > 0) {
      let productEdit: IProductEdit = {
        productId:  product.productId,
        name: product.name,
        code: product.code,
        unitPrice: product.unitPrice,
        description: product.description,
        categoryId: selectedCategoryId
      }
      this.productDataService.updateProduct(productEdit).subscribe(() => {
        this.router.navigate(['/product/product-list']);
      });
    }
    else {
      // let produtCreate: IProductCreate = map(
      //                       p => {
      //                         var result:  IProductCreate = {
      //                           name: product.name,
      //                           unitPrice: product.unitPrice,
      //                           description: product.description,
      //                           categoryId: selectedCategoryId
      //                         }
      //                         return result;
      //                       });
      let produtCreate: IProductCreate = {
            name: product.name,
            code: product.code,
            unitPrice: product.unitPrice,
            description: product.description,
            categoryId: selectedCategoryId
          }
      this.productDataService.createProduct(produtCreate).subscribe((savedProduct: IProduct) => {
        this.router.navigate(['/product/product-list']);
      });
    }
  }

  close() {
    this.router.navigate(['/product/product-list']);
  }
}