import {Component, OnInit} from '@angular/core';
import {TestRequestService, TestResultService, TestTypeDto} from "../../shared/services/swagger";
import {ActivatedRoute} from "@angular/router";
import {NotificationService} from "../../shared/services/notification.service";
import {Location} from "@angular/common";


@Component({
  selector: 'app-test-result-create',
  templateUrl: './test-result-create.component.html',
  styleUrl: './test-result-create.component.scss'
})
export class TestResultCreateComponent implements OnInit {
  selectedFiles: File[] = [];
  remainingTestTypes: TestTypeDto[] = [];
  selectedTestTypesIds: number[] = [];
  testRequestId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private testResultService: TestResultService,
    private testRequestService: TestRequestService,
    private notificationService: NotificationService
  ) {
  }

  ngOnInit(): void {
    this.getRemainingTestTypes();
  }

  uploadFile() {
    if (!this.selectedFiles) {
      this.notificationService.error("Please select a file");
    }
    this.testResultService.addTestResultForm(this.testRequestId, this.selectedTestTypesIds, this.selectedFiles[0]).subscribe({
      next: (result) => {
        this.notificationService.success("Test result uploaded successfully");
        this.location.back();
      }, error: err => {
        this.notificationService.error("Error uploading test result", err);
      }
    });
  }

  private getRemainingTestTypes(): void {
    this.route.paramMap.subscribe(params => {
      this.testRequestId = Number(params.get('testRequestId'));
      this.testRequestService.getRemainingTestTypes(this.testRequestId).subscribe({
        next: (result: TestTypeDto[]) => {
          this.remainingTestTypes = result;
        }
      })
    });
  }

}
