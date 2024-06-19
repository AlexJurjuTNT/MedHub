import {Component, OnInit} from '@angular/core';
import {TestRequestService, TestResultService, TestTypeDto} from "../../shared/services/swagger";
import {ActivatedRoute} from "@angular/router";


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
    private testResultService: TestResultService,
    private testRequestService: TestRequestService,
  ) {
  }

  ngOnInit(): void {
    this.getRemainingTestTypes();
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


  uploadFile() {
    if (!this.selectedFiles) {
      alert("Please select a file");
    }

    this.testResultService.addTestResultForm(this.testRequestId, this.selectedTestTypesIds, this.selectedFiles[0]).subscribe({});

    alert("Uploaded test result");
  }

}
