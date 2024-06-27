import {Component, OnInit} from '@angular/core';
import {AuthenticationService, GroupingInfo, PatientRegisterRequest, PatientService, SortingInfo, SummaryInfo, UserDto, UserService} from "../../shared/services/swagger";
import {Router} from "@angular/router";
import {lastValueFrom} from "rxjs";
import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';
import {LoadOptions} from 'devextreme/data';
import {TokenService} from "../../shared/services/token.service";
import {NotificationService} from "../../shared/services/notification.service";

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  customDataSource: DataSource;

  selectedRowKeys: any[] = [];
  selectedPatient: UserDto | null = null;

  createPatientPopupVisible: boolean = false;

  constructor(
    private patientService: PatientService,
    private userService: UserService,
    private tokenService: TokenService,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router
  ) {
    this.customDataSource = new DataSource({
      store: new CustomStore({
        key: 'id',
        load: async (loadOptions: LoadOptions) => {
          try {
            let response = await lastValueFrom(
              this.patientService.getAllPatientsOfClinic(
                this.tokenService.getClinicId(), // clinicId
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

  onSelectionChanged(e: any) {
    this.selectedPatient = e.selectedRowsData[0];
  }

  createPatient(patientRegisterRequest: PatientRegisterRequest) {
    patientRegisterRequest.clinicId = this.tokenService.getClinicId();

    this.authenticationService.registerPatient(patientRegisterRequest).subscribe({
      next: () => {
        this.createPatientPopupVisible = false;
        this.customDataSource.reload();
        this.notificationService.success("Successfully Registered patient");
      }, error: (error) => {
        this.notificationService.error("Successfully Registered patient");
      }
    })
  }
}
