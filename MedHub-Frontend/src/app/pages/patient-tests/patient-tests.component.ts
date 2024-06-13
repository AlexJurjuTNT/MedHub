import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {PatientDto, PatientService, TestRequestDto, TestTypeDto} from "../../shared/services/swagger";

@Component({
  selector: 'app-patient-tests',
  templateUrl: './patient-tests.component.html',
  styleUrl: './patient-tests.component.scss'
})
export class PatientTestsComponent implements OnInit {
  userId: number = 0;
  patient: PatientDto = {} as PatientDto;
  dataSource: TestRequestDto[] = [];

  constructor(
    private route: ActivatedRoute,
    private patientService: PatientService,
    private router: Router
  ) {
  }

  // todo: paging
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('id'));
      this.patientService.getTestRequestsOfPatient(this.userId).subscribe({
        next: (result: TestRequestDto[]) => {
          this.dataSource = result;
          console.log(this.dataSource)
        }
      });
    });
  }

  getTestTypeNames(testTypes: TestTypeDto[]): string {
    return testTypes.map(testType => testType.name).join(', ');
  }

  navigateToTestResultCreate(testRequestId: number) {
    this.router.navigate(['pages/test-result-create', testRequestId]);
  }

  // todo: do something for multiple pdfs for a request
  navigateToViewTestResults(testRequestId: number) {
    this.router.navigate(['pages/test-result-view', this.dataSource.find(d => d.id == testRequestId)?.testResults[0].id]);
  }
}
