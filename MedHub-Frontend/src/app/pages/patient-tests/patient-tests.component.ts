import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import DataSource from "devextreme/data/data_source";
import CustomStore from "devextreme/data/custom_store";
import {LoadOptions} from "devextreme/data";
import {lastValueFrom} from "rxjs";
import {
  AddTestRequestDto,
  ClinicService,
  GroupingInfo,
  LaboratoryDto,
  SortingInfo,
  SummaryInfo,
  TestRequestDto,
  TestRequestService,
  TestTypeDto,
  TestTypeService,
  UserDto,
  UserService
} from "../../shared/services/swagger";
import {TokenService} from "../../shared/services/token.service";
import {Role} from "../../shared/services/role.enum";
import {AuthService} from "../../shared/services";
import {NotificationService} from "../../shared/services/notification.service";

@Component({
  selector: 'app-patient-tests',
  templateUrl: './patient-tests.component.html',
  styleUrls: ['./patient-tests.component.scss']
})
export class PatientTestsComponent implements OnInit {
  customDataSource: DataSource;
  user: UserDto = {} as UserDto;
  role: Role | null;
  remainingTestTypes: { [key: number]: TestTypeDto[] } = {};

  createTestRequestPopupVisible: boolean = false;
  doctor: UserDto = {} as UserDto;
  laboratories: LaboratoryDto[] = [];
  testTypes: TestTypeDto[] = [];
  protected readonly Role = Role;

  constructor(
    private route: ActivatedRoute,
    private testRequestService: TestRequestService,
    private tokenService: TokenService,
    private testTypeService: TestTypeService,
    private authService: AuthService,
    private clinicService: ClinicService,
    private userService: UserService,
    private notificationService: NotificationService,
    private router: Router
  ) {

    this.route.paramMap.subscribe(params => {
      const userId = Number(params.get('id'));
      this.userService.getUserById(userId).subscribe({
        next: result => {
          this.user = result;
        }
      });
    });

    this.role = this.tokenService.getUserRole();

    this.customDataSource = new DataSource({
      store: new CustomStore({
        key: 'id',
        load: async (loadOptions: LoadOptions) => {
          try {
            let response = await lastValueFrom(
              this.testRequestService.getAllTestRequestsOfUserInClinic(
                this.user.id,
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

            for (let testRequest of response.data!) {
              this.getRemainingTestTypes(testRequest.id);
            }

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
          return lastValueFrom(this.testRequestService.deleteTestRequest(key));
        }
      }),
    });
  }

  getRemainingTestTypes(testRequestId: number): void {
    this.testRequestService.getRemainingTestTypes(testRequestId).subscribe({
      next: (result: TestTypeDto[]) => {
        this.remainingTestTypes[testRequestId] = result;
      }
    });
  }

  ngOnInit(): void {
    this.getLaboratories();
    this.getTestTypes();
    this.getCurrentUser();
  }

  getTestTypeNames(testTypes: TestTypeDto[]): string {
    return testTypes.map(testType => testType.name).join(', ');
  }

  navigateToViewTestResult(testResultId: number) {
    this.router.navigate(['pages/test-result-view', testResultId]);
  }

  navigateToAddTestResult(testRequestId: number, remainingTestTypes: TestTypeDto[]) {
    this.router.navigate(['pages/test-result-create', testRequestId], {state: {remainingTestTypes}});
  }

  getDoctorName(testRequestDto: TestRequestDto): string {
    return testRequestDto.doctor ? testRequestDto.doctor.email : 'N/A';
  }

  getLaboratoryLocation(testRequestDto: TestRequestDto): string {
    return testRequestDto.laboratory ? testRequestDto.laboratory.location : 'N/A';
  }

  createTestRequest(addTestRequestDto: AddTestRequestDto) {
    console.log(addTestRequestDto);

    this.testRequestService.createTestRequest(addTestRequestDto).subscribe({
      next: value => {
        this.customDataSource.reload();
        this.createTestRequestPopupVisible = false;
        this.notificationService.success("Successfully Create Test Request");
      }, error: (error) => {
        this.notificationService.error("Error creating test request");
      }
    });
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
  private getCurrentUser() {
    this.authService.getUser().then((e) => {
      if (e.data) {
        this.doctor = e.data;
      }
    });
  }
}
