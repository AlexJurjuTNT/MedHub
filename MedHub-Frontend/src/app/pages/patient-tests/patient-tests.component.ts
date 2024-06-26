import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {TestRequestDto, TestRequestService, TestTypeDto} from "../../shared/services/swagger";
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
  dataSource: TestRequestDto[] = [];
  remainingTestTypes: { [key: number]: TestTypeDto[] } = {};

  constructor(
    private route: ActivatedRoute,
    private testRequestService: TestRequestService,
    private tokenService: TokenService,
    private router: Router
  ) {
  }

// todo: improve this, dont chain calls
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('id'));
      this.role = this.tokenService.getUserRole();
      this.getTestRequestsOfPatient();
    });
  }

  private getTestRequestsOfPatient() {
    this.clinicId = this.tokenService.getClinicId();
    this.testRequestService.getAllTestRequestsOfUserInClinic(this.userId, this.clinicId).subscribe({
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

  getDoctorName(testRequestDto: TestRequestDto): string {
    return testRequestDto.doctor ? testRequestDto.doctor.email : 'N/A';
  }

  getLaboratoryLocation(testRequestDto: TestRequestDto): string {
    return testRequestDto.laboratory ? testRequestDto.laboratory.location : 'N/A';
  }
}
