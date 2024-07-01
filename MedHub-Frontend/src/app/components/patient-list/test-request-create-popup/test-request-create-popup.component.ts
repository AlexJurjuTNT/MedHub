import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CreateTestRequestRequest, LaboratoryDto, TestTypeDto, UserDto} from "../../../shared/services/swagger";

@Component({
  selector: 'app-test-request-create-popup',
  templateUrl: './test-request-create-popup.component.html',
  styleUrl: './test-request-create-popup.component.scss'
})
export class TestRequestCreatePopupComponent {
  @Input() visible: boolean = false;
  @Input() doctor: UserDto = {} as UserDto;
  @Input() patient: UserDto = {} as UserDto;
  @Input() laboratories: LaboratoryDto[] = [];

  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createTestRequest = new EventEmitter<CreateTestRequestRequest>();

  selectedTestTypesIds: number[] = [];
  selectedLaboratoryId: number = 0;
  availableTestTypes: TestTypeDto[] = [];

  onHiding() {
    this.visibleChange.emit(false);
  }

  onLaboratoryChange(e: any) {
    this.selectedLaboratoryId = e.value;
    this.selectedTestTypesIds = []; // Clear selected test types when laboratory changes
    this.updateAvailableTestTypes();
  }

  updateAvailableTestTypes() {
    const selectedLaboratory = this.laboratories.find(lab => lab.id === this.selectedLaboratoryId);
    this.availableTestTypes = selectedLaboratory ? selectedLaboratory.testTypes : [];
  }

  submitTestRequest($event: Event) {
    $event.preventDefault();

    const createTestRequest: CreateTestRequestRequest = {
      testTypesId: this.selectedTestTypesIds,
      laboratoryId: this.selectedLaboratoryId,
      patientId: this.patient.id,
      doctorId: this.doctor.id,
    }

    this.createTestRequest.emit(createTestRequest);
  }
}
