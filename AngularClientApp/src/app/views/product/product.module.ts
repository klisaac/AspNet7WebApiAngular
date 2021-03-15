import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductRoutingModule } from './product-routing.module';



@NgModule({
  declarations: [ProductRoutingModule.components],
  imports: [
    ProductRoutingModule,
    SharedModule
  ]
})
export class ProductModule { }
