import {Component, EventEmitter, Input, Output} from '@angular/core';
import {PatientRegisterDto} from "../../../shared/services/swagger";

@Component({
  selector: 'app-create-patient-popup',
  templateUrl: './create-patient-popup.component.html',
  styleUrl: './create-patient-popup.component.scss'
})
export class CreatePatientPopupComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createPatient = new EventEmitter<PatientRegisterDto>();

  formData: any = {};

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const registerPatientDto: PatientRegisterDto = {
      email: this.formData.email,
      clinicId: 0,
    }

    this.createPatient.emit(registerPatientDto);
  }

}
