import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { AngularGridInstance, Column, GridOption, GraphqlService, GraphqlPaginatedResult, Filters, Formatters, OnEventArgs, Metrics, FieldType } from 'angular-slickgrid';
import { ProductDataService } from 'src/app/core/services/product-data.service';
import { PageService } from 'src/app/core/services/page.service';
import { IProductGridItem } from 'src/app/shared/interfaces';

const GRAPHQL_QUERY_DATASET_NAME = 'products';

@Component({
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit, OnDestroy {
  angularGrid: AngularGridInstance;
  columnDefinitions: Column[];
  gridOptions: GridOption;
  dataset = [];
  gridStateSub: Subscription;

  constructor(private dataService: ProductDataService, private router: Router, private pageService: PageService) {
  }

  ngOnDestroy() {
    this.gridStateSub.unsubscribe();
  }

  ngOnInit(): void {
    this.columnDefinitions = [
      {
        id:'edit',
        field: 'id',
        name:'Edit',
        excludeFromColumnPicker: true,
        excludeFromGridMenu: true,
        excludeFromHeaderMenu: true,
        formatter: Formatters.editIcon,
        maxWidth: 50,
        cssClass: "Edit",
         onCellClick: (e: Event, args: OnEventArgs) => {
          this.router.navigate(['/product/product-edit/' + args.dataContext.id]);
        }
      },
      // { id: 'productId', field: 'id', name: 'productId', filterable: true, sortable: true, maxWidth: 200, type: FieldType.number, filter: { model: Filters.inputNumber } },
      { id: 'name', field: 'name', name: 'Name', maxWidth:175, filterable: true, sortable: true },
      { id: 'code', field: 'code', name: 'Code', maxWidth:175, filterable: true, sortable: true },
      { id: 'unitPrice', field: 'unitPrice', name: 'Unit Price', maxWidth:100, filterable: true, sortable: true, filter: { model: Filters.inputNumber } },
      { id: 'category', field: 'category.name', name: 'Category', maxWidth:175, filterable: true, sortable: true, formatter: Formatters.complexObject },
      { id: 'description', field: 'description', name: 'Description', maxWidth: 275, filterable: true, sortable: true }
    ];

    this.gridOptions = {
      enableAutoResize: true,
      enableAutoSizeColumns:true,
      autoFitColumnsOnFirstLoad: true,
      autoResize: {
        containerId: 'grid-container',
        sidePadding: 10,
        maxWidth:1050,
        maxHeight:500,
        calculateAvailableSizeBy: 'window'
      },
      backendServiceApi: {
        service: new GraphqlService(),
        options: {
          columnDefinitions: this.columnDefinitions,
          datasetName: GRAPHQL_QUERY_DATASET_NAME
        },
        process: (query) => this.getProducts()
      }
    };
  }

  angularGridReady(angularGrid: AngularGridInstance) {
    this.angularGrid = angularGrid;
    this.gridStateSub = this.angularGrid.gridStateService.onGridStateChanged.subscribe((data) => console.log(data));
  }

  getProducts(): Observable<GraphqlPaginatedResult> {
    var args = this.pageService.getPageArgs(this.angularGrid);
    return this.dataService.searchProducts(args)
      .pipe(map(
        page => {
          var result: GraphqlPaginatedResult = {
            data: {
              [GRAPHQL_QUERY_DATASET_NAME]:{
                nodes: page.items.map<IProductGridItem>(({productId, name, code, unitPrice, description, category}) => ({id: productId, name: name, code: code, unitPrice:unitPrice, description: description, category: category})),
                totalCount: page.totalCount
              }
            }
          };

          return result;
        }));
  }
}