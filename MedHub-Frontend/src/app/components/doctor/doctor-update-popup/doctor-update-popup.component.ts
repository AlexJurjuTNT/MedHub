import {Component, EventEmitter, Input, Output} from '@angular/core';
import {UpdateUserRequest, UserDto} from "../../../shared/services/swagger";

@Component({
  selector: 'app-doctor-update-popup',
  templateUrl: './doctor-update-popup.component.html',
  styleUrl: './doctor-update-popup.component.scss'
})
export class DoctorUpdatePopupComponent {
  @Input() visible: boolean = false;
  @Input() selectedDoctor: UserDto = {} as UserDto;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() updateDoctor = new EventEmitter<UpdateUserRequest>();

  formData: UpdateUserRequest = {
    id: this.selectedDoctor.id,
    email: '',
    username: '',
    clinicId: this.selectedDoctor.clinicId,
    firstName: '',
    familyName: '',
  };

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const updateUserRequest: UpdateUserRequest = {
      id: this.selectedDoctor.id,
      email: this.formData.email,
      username: this.formData.username,
      clinicId: this.formData.clinicId,
      firstName: this.formData.firstName,
      familyName: this.formData.familyName,
    };

    this.updateDoctor.emit(updateUserRequest);
  }
}
