import {Component, OnInit} from '@angular/core';
import {AuthenticationService, DoctorService, GroupingInfo, SortingInfo, SummaryInfo, UpdateUserRequest, UserDto, UserRegisterDto} from "../../shared/services/swagger";
import DataSource from "devextreme/data/data_source";
import CustomStore from "devextreme/data/custom_store";
import {LoadOptions} from "devextreme/data";
import {lastValueFrom} from "rxjs";
import {NotificationService} from "../../shared/services/notification.service";

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {
  customDataSource: DataSource;

  createPopupVisible: boolean = false;
  updatePopupVisible: boolean = false;

  selectedRowKeys: any[] = [];
  selectedRow: UserDto | null = null;

  constructor(
    private doctorService: DoctorService,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
  ) {
    this.customDataSource = new DataSource({
      store: new CustomStore({
        key: 'id',
        load: async (loadOptions: LoadOptions) => {
          try {
            let response = await lastValueFrom(
              this.doctorService.getAllDoctors(
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
          return lastValueFrom(this.doctorService.deleteDoctor(key));
        }
      }),
    });
  }

  ngOnInit(): void {
  }

  createDoctor(userRegisterRequest: UserRegisterDto) {
    this.authenticationService.registerDoctor(userRegisterRequest).subscribe({
      next: () => {
        this.createPopupVisible = false;
        this.customDataSource.reload();
        this.notificationService.success("Successfully created new Doctor");
      }, error: (error) => {
        this.notificationService.error("Failed to create Doctor" + error.message);
      }
    });
  }

  updateDoctor(updateUserRequest: UpdateUserRequest) {
    this.doctorService.updateDoctor(updateUserRequest.id, updateUserRequest).subscribe({
      next: () => {
        this.updatePopupVisible = false;
        this.customDataSource.reload();
        this.notificationService.success("Successfully updated new Doctor");
      }, error: (error) => {
        this.notificationService.error("Error updating doctor " + error.message);
      }
    });
  }

  onSelectionChanged(e: any) {
    this.selectedRow = e.selectedRowsData[0];
  }

}
