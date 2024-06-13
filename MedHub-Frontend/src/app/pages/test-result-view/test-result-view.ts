import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {TestResultService} from "../../shared/services/swagger";
import {DomSanitizer, SafeUrl} from "@angular/platform-browser";

@Component({
  selector: 'app-test-result-view',
  templateUrl: './test-result-view.html',
  styleUrl: './test-result-view.scss'
})
export class TestResultView implements OnInit {
  testResultId: number = 0;
  pdfUrl: SafeUrl = {} as SafeUrl;

  constructor(
    private route: ActivatedRoute,
    private testResultService: TestResultService,
    private sanitizer: DomSanitizer
  ) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.testResultId = Number(params.get('testResultId'));

      this.testResultService.downloadPdf(this.testResultId).subscribe({
        next: (result: Blob) => {
          const unsafeUrl = URL.createObjectURL(result);
          this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(unsafeUrl);
        }
      })
    })
  }

}
