import {Component, OnInit} from '@angular/core';
import {TestResultService} from "../../shared/services/swagger";
import {ActivatedRoute} from "@angular/router";


@Component({
  selector: 'app-test-result-create',
  templateUrl: './test-result-create.component.html',
  styleUrl: './test-result-create.component.scss'
})
export class TestResultCreateComponent implements OnInit {
  testRequestId: number = 0;
  // todo: upload one file or multiple files?
  selectedFiles: File[] = [];

  constructor(
    private route: ActivatedRoute,
    private testResultService: TestResultService
  ) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.testRequestId = Number(params.get('testRequestId'));
    })
  }


  uploadFile() {

    console.log(this.selectedFiles);

    if (this.selectedFiles) {
      this.testResultService.addTestResultForm(this.testRequestId, this.selectedFiles[0]).subscribe({})
    } else {
      alert("Please select a pdf file");
    }
  }

}
