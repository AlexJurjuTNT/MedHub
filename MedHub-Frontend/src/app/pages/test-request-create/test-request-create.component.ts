import {Component, OnInit} from '@angular/core';
import {AddTestRequestDto, ClinicService, LaboratoryDto, PatientService, TestRequestService, TestTypeDto, TestTypeService, UserDto} from "../../shared/services/swagger";
import {AuthService} from "../../shared/services";

@Component({
  selector: 'app-test-request-create',
  templateUrl: './test-request-create.component.html',
  styleUrls: ['./test-request-create.component.css']
})
export class TestRequestCreateComponent implements OnInit {

  users: UserDto[] = [];
  testTypes: TestTypeDto[] = [];
  doctor: UserDto = {} as UserDto;
  laboratories: LaboratoryDto[] = [];

  selectedUserId: number | null = null;
  selectedTestTypesIds: number[] = [];
  selectedLaboratoryId: number | undefined = undefined;

  constructor(
    private patientService: PatientService,
    private testTypeService: TestTypeService,
    private testRequestService: TestRequestService,
    private authService: AuthService,
    private clinicService: ClinicService,
  ) {
  }

  // todo: make it so available testTypes are chosen from the laboratory
  ngOnInit(): void {
    this.authService.getUser().then((e) => {
      if (e.data) {
        this.doctor = e.data;
        this.getUsers();
        this.getTestTypes();
        this.getLaboratoriesOfClinic();
      }
    });
  }

  createTestRequest() {
    if (this.selectedUserId && this.selectedLaboratoryId && this.selectedTestTypesIds.length > 0) {
      const testRequest: AddTestRequestDto = {
        patientId: this.selectedUserId,
        doctorId: this.doctor.id,
        testTypesId: this.selectedTestTypesIds,
        laboratoryId: this.selectedLaboratoryId
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
    this.patientService.getAllPatientsOfClinic(this.doctor.clinicId).subscribe({
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

  private getLaboratoriesOfClinic() {
    this.clinicService.getAllLaboratoriesOfClinic(this.doctor.clinicId).subscribe({
      next: (result: LaboratoryDto[]) => {
        this.laboratories = result;
        console.log('Laboratories:', this.laboratories);
      },
      error: (error) => {
        console.error('Error fetching laboratories:', error);
      }
    });
  }

}
