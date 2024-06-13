import {Component, OnInit} from '@angular/core';
import {AddTestRequestDto, PatientService, TestRequestService, TestTypeDto, TestTypeService, UserDto} from "../../shared/services/swagger";
import {AuthService} from "../../shared/services";

@Component({
  selector: 'app-test-request-create',
  templateUrl: './test-request-create.component.html',
  styleUrls: ['./test-request-create.component.css']
})
export class TestRequestCreateComponent implements OnInit {

  users: UserDto[] = [];
  testTypes: TestTypeDto[] = [];
  selectedUserId: number | null = null;
  selectedTestTypesIds: number[] = [];
  user: UserDto = {} as UserDto;


  constructor(
    private patientService: PatientService,
    private testTypeService: TestTypeService,
    private authService: AuthService,
    private testRequestService: TestRequestService
  ) {
  }

  ngOnInit(): void {
    this.authService.getUser().then((e) => {
      if (e.data) {
        this.user = e.data;
        this.getUsers();
        this.getTestTypes();
      }
    });
  }

  createTestRequest() {
    if (this.selectedUserId && this.selectedTestTypesIds.length > 0) {
      const testRequest: AddTestRequestDto = {
        patientId: this.selectedUserId,
        doctorId: this.user.id,
        testTypesId: this.selectedTestTypesIds
      };

      this.testRequestService.createTestRequest(testRequest).subscribe({
        next: result => {
          alert("Test Request Created");
          location.reload();
        },
        error: err => {
          alert("Failed to create test request")
        }
      })
    }
  }

  private getUsers() {
    this.patientService.getAllPatientsOfClinic(this.user.clinicId).subscribe({
      next: (result) => {
        this.users = result.data!;
      }
    })
  }

  private getTestTypes() {
    this.testTypeService.getAllTestTypes().subscribe({
      next: (result) => {
        this.testTypes = result;
      }
    })
  }
}
