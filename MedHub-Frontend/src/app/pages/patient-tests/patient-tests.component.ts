import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {PatientService, TestRequestDto, TestRequestService, TestTypeDto} from "../../shared/services/swagger";
import {TokenService} from "../../shared/services/token.service";

@Component({
  selector: 'app-patient-tests',
  templateUrl: './patient-tests.component.html',
  styleUrls: ['./patient-tests.component.scss']
})
export class PatientTestsComponent implements OnInit {
  userId: number = 0;

  role: string = "";
  dataSource: TestRequestDto[] = [];
  remainingTestTypes: { [key: number]: TestTypeDto[] } = {};

  constructor(
    private route: ActivatedRoute,
    private patientService: PatientService,
    private testRequestService: TestRequestService,
    private tokenService: TokenService,
    private router: Router
  ) {
  }

// todo: improve this, make calls sync instead of chaining them
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('id'));
      this.getTestRequestsOfPatient();
      this.role = this.tokenService.getUserRole();
    });
  }

  private getTestRequestsOfPatient() {
    this.patientService.getTestRequestsOfPatient(this.userId).subscribe({
      next: (result: TestRequestDto[]) => {
        this.dataSource = result;
        this.loadRemainingTestTypes();
      }
    });
  }

  private loadRemainingTestTypes() {
    this.dataSource.forEach(testRequest => {
      this.testRequestService.getRemainingTestTypes(testRequest.id).subscribe({
        next: (result: TestTypeDto[]) => {
          this.remainingTestTypes[testRequest.id] = result;
        }
      });
    });
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
}
