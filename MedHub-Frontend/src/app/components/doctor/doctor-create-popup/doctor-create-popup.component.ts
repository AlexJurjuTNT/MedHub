import {Component, EventEmitter, Input, Output} from '@angular/core';
import {UserRegisterRequest} from "../../../shared/services/swagger";

@Component({
  selector: 'app-doctor-create-popup',
  templateUrl: './doctor-create-popup.component.html',
  styleUrl: './doctor-create-popup.component.scss'
})
export class DoctorCreatePopupComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createDoctor = new EventEmitter<UserRegisterRequest>();

  formData: UserRegisterRequest = {
    username: '',
    email: '',
    password: '',
    clinicId: 0,
    firstName: '',
    familyName: ''
  };

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();
    const userRegisterRequest: UserRegisterRequest = {
      username: this.formData.username,
      email: this.formData.email,
      password: this.formData.password,
      clinicId: this.formData.clinicId,
      firstName: this.formData.firstName,
      familyName: this.formData.familyName,
    };
    this.createDoctor.emit(userRegisterRequest);
  }
}
