import {Component} from '@angular/core';
import {CreateTestTypeRequest, GroupingInfo, SortingInfo, SummaryInfo, TestTypeDto, TestTypeService} from "../../shared/services/swagger";
import DataSource from "devextreme/data/data_source";
import CustomStore from "devextreme/data/custom_store";
import {LoadOptions} from "devextreme/data";
import {lastValueFrom} from "rxjs";
import {NotificationService} from "../../shared/services/notification.service";
import {TokenService} from "../../shared/services/token.service";
import {Role} from "../../shared/services/role.enum";

@Component({
  selector: 'app-test-type-list',
  templateUrl: './test-type-list.component.html',
  styleUrls: ['./test-type-list.component.css']
})
export class TestTypeListComponent {
  customDataSource: DataSource;

  selectedRowKeys: any[] = [];
  selectedRow: TestTypeDto | null = null;

  updatePopupVisible: boolean = false;
  createPopupVisible: boolean = false;

  isAdmin: boolean;

  constructor(
    private testTypeService: TestTypeService,
    private notificationService: NotificationService,
    private tokenService: TokenService
  ) {
    const role = this.tokenService.getUserRole();
    this.isAdmin = role == Role.Admin;
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

  createTestType(createTestTypeRequest: CreateTestTypeRequest) {
    this.testTypeService.createTestType(createTestTypeRequest).subscribe({
      next: () => {
        this.createPopupVisible = false;
        this.customDataSource.reload();
        this.notificationService.success("Added new Test Type");
      },
      error: (error) => {
        this.notificationService.error("Failed to add new Test Type: " + error.message);
      }
    });
  }

  updateTestType(updatedTestTypeDto: TestTypeDto) {
    this.testTypeService.updateTestType(updatedTestTypeDto.id, updatedTestTypeDto).subscribe({
      next: () => {
        this.updatePopupVisible = false;
        this.customDataSource.reload();
        this.notificationService.success("Updated Test Type");
      },
      error: (error) => {
        this.notificationService.error("Failed to update Test Type: " + error.message);
      }
    });
  }

  onSelectionChanged(e: any) {
    this.selectedRow = e.selectedRowsData[0];
  }
}
