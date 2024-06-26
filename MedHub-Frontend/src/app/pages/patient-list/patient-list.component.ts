import {Component, OnInit} from '@angular/core';
import {AuthenticationService, GroupingInfo, PatientRegisterDto, PatientService, SortingInfo, SummaryInfo, UserDto, UserService} from "../../shared/services/swagger";
import {AuthService} from "../../shared/services";
import {Router} from "@angular/router";
import {lastValueFrom} from "rxjs";
import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';
import {LoadOptions} from 'devextreme/data';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  customDataSource: DataSource;
  doctor: UserDto = {} as UserDto;
  selectedRowKeys: any[] = [];
  selectedRow: any;
  createPopupVisible: boolean = false;

  constructor(
    private patientService: PatientService,
    private userService: UserService,
    private authService: AuthService,
    private authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.loadCurrentUser();
    this.customDataSource = new DataSource({
      store: new CustomStore({
        key: 'id',
        load: async (loadOptions: LoadOptions) => {
          try {
            let response = await lastValueFrom(
              this.patientService.getAllPatientsOfClinic(
                this.doctor.clinicId, // clinicId
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
          return lastValueFrom(this.userService.deleteUser(key));
        }
      }),
    });
  }

  ngOnInit(): void {
  }

  navigateToPatientTests(patientId: number): void {
    this.router.navigate(['pages/patient-tests', patientId]);
  }

  navigateToPatientInformation(userId: number) {
    this.router.navigate(["/patient-info", userId]);
  }

  private loadCurrentUser() {
    let result = this.authService.getUserSync();
    if (result.isOk) {
      this.doctor = result.data!;
    }
  }

  onSelectionChanged(e: any) {
    this.selectedRow = e.selectedRowsData[0];
  }

  createPatient(patientRegisterDto: PatientRegisterDto) {
    this.authenticationService.registerPatient(patientRegisterDto).subscribe({
      next: () => {
        this.createPopupVisible = false;
        this.customDataSource.reload();
      }
    })
  }
}
