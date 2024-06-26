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

  updateDoctorFormData: any = {};

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();

    const updateUserRequest: UpdateUserRequest = {
      id: this.selectedDoctor.id,
      email: this.updateDoctorFormData.email,
      username: this.updateDoctorFormData.username,
      clinicId: this.updateDoctorFormData.clinicId
    };

    this.updateDoctor.emit(updateUserRequest);
  }
}
