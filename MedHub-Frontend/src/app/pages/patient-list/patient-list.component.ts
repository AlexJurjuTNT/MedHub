import {Component, OnInit} from '@angular/core';
import {
  AddTestRequestDto,
  AuthenticationService,
  ClinicService,
  GroupingInfo,
  LaboratoryDto,
  PatientRegisterDto,
  PatientService,
  SortingInfo,
  SummaryInfo,
  TestRequestService,
  TestTypeDto,
  TestTypeService,
  UserDto,
  UserService
} from "../../shared/services/swagger";
import {Router} from "@angular/router";
import {lastValueFrom} from "rxjs";
import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';
import {LoadOptions} from 'devextreme/data';
import {TokenService} from "../../shared/services/token.service";
import {AuthService} from "../../shared/services";

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

  createTestRequestPopupVisible: boolean = false;
  doctor: UserDto = {} as UserDto;
  laboratories: LaboratoryDto[] = [];
  testTypes: TestTypeDto[] = [];

  constructor(
    private patientService: PatientService,
    private testRequestService: TestRequestService,
    private testTypeService: TestTypeService,
    private userService: UserService,
    private tokenService: TokenService,
    private authService: AuthService,
    private clinicService: ClinicService,
    private authenticationService: AuthenticationService,
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
    this.getLaboratories();
    this.getTestTypes();
    this.getDoctor();
  }

  private getLaboratories() {
    const clinicId = this.tokenService.getClinicId();
    this.clinicService.getAllLaboratoriesOfClinic(clinicId).subscribe({
      next: result => {
        this.laboratories = result;
      }
    });
  }

  private getTestTypes() {
    this.testTypeService.getAllTestTypes().subscribe({
      next: result => {
        this.testTypes = result.data!;
      }
    });
  }

  // todo: if admin does this then make it so the admin can chose a doctor in the popup
  private getDoctor() {
    this.authService.getUser().then((e) => {
      if (e.data) {
        this.doctor = e.data;
      }
    });
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

  createPatient(patientRegisterDto: PatientRegisterDto) {
    patientRegisterDto.clinicId = this.tokenService.getClinicId();

    this.authenticationService.registerPatient(patientRegisterDto).subscribe({
      next: () => {
        this.createPatientPopupVisible = false;
        this.customDataSource.reload();
      }
    })
  }

  createTestRequest(addTestRequestDto: AddTestRequestDto) {
    this.testRequestService.createTestRequest(addTestRequestDto).subscribe({
      next: value => {
        this.customDataSource.reload();
        this.createPatientPopupVisible = false;
      }
    });
  }
}
