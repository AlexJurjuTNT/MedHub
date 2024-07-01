import {Component, OnInit} from '@angular/core';
import {TestRequestService, TestResultService, TestTypeDto, UserDto, UserService} from "../../shared/services/swagger";
import {ActivatedRoute, Router} from "@angular/router";
import {NotificationService} from "../../shared/services/notification.service";
import {map, mergeMap} from 'rxjs/operators';
import {forkJoin} from 'rxjs';

@Component({
  selector: 'app-test-result-create',
  templateUrl: './test-result-create.component.html',
  styleUrl: './test-result-create.component.scss'
})
export class TestResultCreateComponent implements OnInit {
  selectedFiles: File[] = [];
  remainingTestTypes: TestTypeDto[] = [];
  selectedTestTypesIds: number[] = [];
  testResultId: number = 0;
  patientId: number = 0;
  patient: UserDto = {} as UserDto;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private testResultService: TestResultService,
    private testRequestService: TestRequestService,
    private userService: UserService,
    private notificationService: NotificationService
  ) {
  }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      map(params => ({
        testRequestId: Number(params.get('testRequestId')),
        patientId: Number(params.get('patientId'))
      })),
      mergeMap(({testRequestId, patientId}) => {
        this.testResultId = testRequestId;
        this.patientId = patientId;
        return forkJoin({
          remainingTestTypes: this.testRequestService.getRemainingTestTypes(testRequestId),
          patient: this.userService.getUserById(patientId)
        });
      })
    ).subscribe({
      next: ({remainingTestTypes, patient}) => {
        this.remainingTestTypes = remainingTestTypes;
        this.patient = patient;
        console.log('Patient details:', this.patient);
      },
      error: (error) => {
        this.notificationService.error("Error fetching data", error);
      }
    });
  }

  uploadFile() {
    if (this.selectedFiles.length == 0) {
      this.notificationService.error("Please select a file");
    } else if (this.selectedTestTypesIds.length == 0) {
      this.notificationService.error("Please select at least one test type");
    } else {
      this.testResultService.addTestResultForm(this.testResultId, this.selectedTestTypesIds, this.selectedFiles[0]).subscribe({
        next: () => {
          this.notificationService.success("Test result uploaded successfully");
          this.goBackToPatientTests();
        },
        error: err => {
          this.notificationService.error("Error uploading test result", err);
        }
      });
    }
  }

  goBackToPatientTests() {
    this.router.navigate(['/pages/patient-tests', this.patientId]);
  }
}
