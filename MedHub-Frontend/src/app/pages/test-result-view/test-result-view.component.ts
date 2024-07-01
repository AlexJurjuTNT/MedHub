import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {TestResultService, UserDto, UserService} from "../../shared/services/swagger";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {catchError, map, Observable, of, switchMap, tap} from "rxjs";

@Component({
  selector: 'app-test-result-view',
  templateUrl: './test-result-view.component.html',
  styleUrl: './test-result-view.component.scss'
})
export class TestResultViewComponent implements OnInit {
  testResultId: number = 0;
  userId: number = 0;
  pdfUrl: SafeResourceUrl | null = null;
  patient: UserDto = {} as UserDto;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private testResultService: TestResultService,
    private sanitizer: DomSanitizer
  ) {
  }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        this.testResultId = Number(params.get('testResultId'));
        this.userId = Number(params.get('patientId'));
        return this.userService.getUserById(this.userId);
      }),
      tap(patient => {
        this.patient = patient;
      }),
      switchMap(() => this.downloadPdf(this.testResultId)),
      catchError(error => {
        console.error("Error in ngOnInit:", error);
        return of(null);
      })
    ).subscribe({
      next: (result) => console.log("PDF download complete", result),
      error: (err) => console.error("Subscription error:", err),
    });
  }


  private downloadPdf(testResultId: number): Observable<void> {
    return this.testResultService.downloadPdf(testResultId).pipe(
      tap(result => console.log("PDF download result:", result)),
      map((result: Blob) => {
        const unsafeUrl = URL.createObjectURL(result);
        this.pdfUrl = this.sanitizer.bypassSecurityTrustResourceUrl(unsafeUrl);
      }),
      catchError(error => {
        console.error("Error downloading PDF:", error);
        this.pdfUrl = null;
        return of(undefined);
      })
    );
  }

  goBackToPatientTests() {
    this.router.navigate(['/pages/patient-tests', this.userId]);
  }
}
