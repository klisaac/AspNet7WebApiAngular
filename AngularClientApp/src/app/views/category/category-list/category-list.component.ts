import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { AngularGridInstance, Column, GridOption, GraphqlService, GraphqlPaginatedResult, Filters, Formatters, OnEventArgs, FieldType } from 'angular-slickgrid';
import { CategoryDataService } from 'src/app/core/services/category-data.service';
import { PageService } from 'src/app/core/services/page.service';
import { ICategoryGridItem } from 'src/app/shared/interfaces';

const GRAPHQL_QUERY_DATASET_NAME = 'categories';

@Component({
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit, OnDestroy {
  angularGrid: AngularGridInstance;
  columnDefinitions: Column[];
  gridOptions: GridOption;
  dataset = [];
  gridStateSub: Subscription;

  constructor(private dataService: CategoryDataService, private router: Router, private pageService: PageService) {
  }

  ngOnInit(): void {
    this.columnDefinitions = [
      {
        id: 'edit',
        field: 'id',
        name:'Edit',
        excludeFromColumnPicker: true,
        excludeFromGridMenu: true,
        excludeFromHeaderMenu: true,
        formatter: Formatters.editIcon,
        minWidth: 50,
        maxWidth: 50,
        onCellClick: (e: Event, args: OnEventArgs) => {
          this.router.navigate(['/category/category-detail/' + args.dataContext.id]);
        }
      },
      { id: 'categoryId', field: 'id', name: 'CategoryId', filterable: true, sortable: true, maxWidth: 100, type: FieldType.number, filter: { model: Filters.inputNumber } },
      { id: 'name', field: 'name', name: 'Name', filterable: true, sortable: true, maxWidth: 300 },
      { id: 'description', field: 'description', name: 'Description', filterable: true, sortable: true, maxWidth: 500 },
    ];

    this.gridOptions = {
      enableAutoResize: true,
      enableAutoSizeColumns:true,
      autoFitColumnsOnFirstLoad: true,
      autoResize: {
        containerId: 'grid-container',
        sidePadding: 10,
        maxWidth:1050,
        calculateAvailableSizeBy: 'window'
      },
      backendServiceApi: {
        service: new GraphqlService(),
        options: {
          columnDefinitions: this.columnDefinitions,
          datasetName: GRAPHQL_QUERY_DATASET_NAME
        },
        process: (query) => this.getCategories(),
      }
    };;
  }

  ngOnDestroy() {
    this.gridStateSub.unsubscribe();
  }

  angularGridReady(angularGrid: AngularGridInstance) {
    this.angularGrid = angularGrid;
    this.gridStateSub = this.angularGrid.gridStateService.onGridStateChanged.subscribe((data) => console.log(data));
  }

  getCategories(): Observable<GraphqlPaginatedResult> {
    var args = this.pageService.getPageArgs(this.angularGrid);

    return this.dataService.searchCategories(args)
      .pipe(map(
        page => {
          var result: GraphqlPaginatedResult = {
          data: {
            [GRAPHQL_QUERY_DATASET_NAME]:{
              nodes: page.items.map<ICategoryGridItem>(({categoryId, name, description}) => ({id: categoryId, name: name, description: description})),
              totalCount: page.totalCount
            }
          }
        };

        return result;
        }));
  }
}
