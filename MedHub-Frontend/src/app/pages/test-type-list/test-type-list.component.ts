import {Component} from '@angular/core';
import {AddTestTypeDto, GroupingInfo, SortingInfo, SummaryInfo, TestTypeDto, TestTypeService} from "../../shared/services/swagger";
import DataSource from "devextreme/data/data_source";
import CustomStore from "devextreme/data/custom_store";
import {LoadOptions} from "devextreme/data";
import {lastValueFrom} from "rxjs";

@Component({
  selector: 'app-test-type-list',
  templateUrl: './test-type-list.component.html',
  styleUrls: ['./test-type-list.component.css']
})
export class TestTypeListComponent {

  customDataSource: DataSource;

  selectedRowKeys: any[] = [];
  selectedRow: TestTypeDto = {} as TestTypeDto;

  updatePopupVisible: boolean = false;
  updateTestTypeFormData: any = {};

  createPopupVisible: boolean = false;
  createTestTypeFormData: any = {};

  constructor(
    private testTypeService: TestTypeService
  ) {
    this.customDataSource = new DataSource({
      store: new CustomStore({
        key: 'id',
        load: async (loadOptions: LoadOptions) => {
          try {
            let response = await lastValueFrom(
              this.testTypeService.getAllTestTypes(
                loadOptions.requireTotalCount,
                loadOptions.requireGroupCount,
                false, // isCountQuery
                false, // isSummaryQuery
                loadOptions.skip,
                loadOptions.take,
                loadOptions.sort as SortingInfo[],
                loadOptions.group as GroupingInfo[],
                loadOptions.filter,
                loadOptions.totalSummary as SummaryInfo[],
                loadOptions.groupSummary as SummaryInfo[],
                loadOptions.select as string[],
                undefined, // preSelect
                undefined, // remoteSelect
                undefined, // remoteGrouping
                undefined, // expandLinqSumType
                undefined, // primaryKey
                undefined, // defaultSort
                undefined, // stringToLower
                undefined, // paginateViaPrimaryKey
                undefined, // sortByPrimaryKey
                undefined  // allowAsyncOverSync
              )
            );
            return {
              data: response.data,
              totalCount: response.totalCount,
              summary: response.summary,
              groupCount: response.groupCount,
            };
          } catch (e) {
            throw 'Data loading error';
          }
        },
        remove: (key) => {
          return lastValueFrom(this.testTypeService.deleteTestType(key));
        }
      }),
    });
  }

  createTestType($event: SubmitEvent) {
    $event.preventDefault();

    const addTestTypeDto: AddTestTypeDto = {
      name: this.createTestTypeFormData.name
    }

    this.testTypeService.createTestType(addTestTypeDto).subscribe({
      next: () => {
        this.createPopupVisible = false;
        this.customDataSource.reload();
      }
    })
  }

  updateTestType($event: SubmitEvent) {
    $event.preventDefault();

    const updatedTestTypeDto: TestTypeDto = {
      name: this.updateTestTypeFormData.name,
      id: this.selectedRow.id
    };

    this.testTypeService.updateTestType(this.selectedRow.id, updatedTestTypeDto).subscribe({
      next: () => {
        this.updatePopupVisible = false;
        this.customDataSource.reload();
      }
    })

  }

  onSelectionChanged(e: any) {
    this.selectedRow = e.selectedRowsData[0];
  }
}
