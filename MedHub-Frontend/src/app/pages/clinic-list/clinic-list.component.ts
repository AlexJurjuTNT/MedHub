import {Component, OnInit} from '@angular/core';
import {AddClinicDto, ClinicDto, ClinicService, GroupingInfo, SortingInfo, SummaryInfo, UpdateClinicDto} from "../../shared/services/swagger";
import DataSource from "devextreme/data/data_source";
import CustomStore from "devextreme/data/custom_store";
import {LoadOptions} from "devextreme/data";
import {lastValueFrom} from "rxjs";

@Component({
  selector: 'app-clinic-list',
  templateUrl: './clinic-list.component.html',
  styleUrls: ['./clinic-list.component.css']
})
export class ClinicListComponent implements OnInit {
  customDataSource: DataSource;

  createPopupVisible: boolean = false;
  updatePopupVisible: boolean = false;

  selectedRowKeys: any[] = [];
  selectedRow: ClinicDto = {} as ClinicDto;

  constructor(
    private clinicService: ClinicService,
  ) {
    this.customDataSource = new DataSource({
      store: new CustomStore({
        key: 'id',
        load: async (loadOptions: LoadOptions) => {
          try {
            let response = await lastValueFrom(
              this.clinicService.getAllClinics(
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
          return lastValueFrom(this.clinicService.deleteClinic(key));
        }
      }),
    });
  }

  ngOnInit(): void {
  }

  createClinic(addClinicDto: AddClinicDto) {
    this.clinicService.createClinic(addClinicDto).subscribe({
      next: () => {
        this.createPopupVisible = false;
        this.customDataSource.reload();
      }
    });
  }

  updateClinic(updateClinicDto: UpdateClinicDto) {
    this.clinicService.updateClinic(updateClinicDto.id, updateClinicDto).subscribe({
      next: () => {
        this.updatePopupVisible = false;
        this.customDataSource.reload();
      }
    });
  }

  onSelectionChanged(e: any) {
    this.selectedRow = e.selectedRowsData[0];
  }
}
