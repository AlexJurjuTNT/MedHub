import {Component, EventEmitter, Input, Output} from '@angular/core';
import {PatientRegisterRequest} from "../../../shared/services/swagger";

@Component({
  selector: 'app-create-patient-popup',
  templateUrl: './create-patient-popup.component.html',
  styleUrl: './create-patient-popup.component.scss'
})
export class CreatePatientPopupComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createPatient = new EventEmitter<PatientRegisterRequest>();

  formData: PatientRegisterRequest = {
    email: '',
    clinicId: 0,
    firstName: '',
    familyName: ''
  };

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const patientRegisterRequest: PatientRegisterRequest = {
      email: this.formData.email,
      familyName: this.formData.familyName,
      firstName: this.formData.firstName,
      clinicId: 0,
    }

    this.createPatient.emit(patientRegisterRequest);
  }

}
