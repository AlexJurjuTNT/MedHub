import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import DataSource from "devextreme/data/data_source";
import CustomStore from "devextreme/data/custom_store";
import {LoadOptions} from "devextreme/data";
import {lastValueFrom} from "rxjs";
import {GroupingInfo, SortingInfo, SummaryInfo, TestRequestDto, TestRequestService, TestTypeDto} from "../../shared/services/swagger";
import {TokenService} from "../../shared/services/token.service";

@Component({
  selector: 'app-patient-tests',
  templateUrl: './patient-tests.component.html',
  styleUrls: ['./patient-tests.component.scss']
})
export class PatientTestsComponent implements OnInit {
  userId: number = 0;
  clinicId: number = 0;
  role: string = "";
  remainingTestTypes: { [key: number]: TestTypeDto[] } = {};

  customDataSource: DataSource;


  constructor(
    private route: ActivatedRoute,
    private testRequestService: TestRequestService,
    private tokenService: TokenService,
    private router: Router
  ) {

    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('id'));
    });

    this.role = this.tokenService.getUserRole();

    this.customDataSource = new DataSource({
      store: new CustomStore({
        key: 'id',
        load: async (loadOptions: LoadOptions) => {
          try {
            let response = await lastValueFrom(
              this.testRequestService.getAllTestRequestsOfUserInClinic(
                this.userId,
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
}
