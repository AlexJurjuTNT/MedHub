import {Component, EventEmitter, Input, Output} from '@angular/core';
import {UserRegisterDto} from "../../../shared/services/swagger";

@Component({
  selector: 'app-doctor-create-popup',
  templateUrl: './doctor-create-popup.component.html',
  styleUrl: './doctor-create-popup.component.scss'
})
export class DoctorCreatePopupComponent {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() createDoctor = new EventEmitter<UserRegisterDto>();

  createDoctorFormData: any = {};

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();
    const addDoctorDto: UserRegisterDto = {
      username: this.createDoctorFormData.username,
      password: this.createDoctorFormData.password,
      email: this.createDoctorFormData.email,
      clinicId: this.createDoctorFormData.clinicId
    };
    this.createDoctor.emit(addDoctorDto);
  }
}
