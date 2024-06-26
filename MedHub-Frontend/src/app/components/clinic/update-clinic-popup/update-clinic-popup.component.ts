import {Component, EventEmitter, Input, Output} from '@angular/core';
import {ClinicDto, UpdateClinicDto} from "../../../shared/services/swagger";

@Component({
  selector: 'app-update-clinic-popup',
  templateUrl: './update-clinic-popup.component.html'
})
export class UpdateClinicPopupComponent {
  @Input() visible: boolean = false;
  @Input() selectedClinic: ClinicDto = {} as ClinicDto;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() updateClinic = new EventEmitter<UpdateClinicDto>();

  updateClinicFormData: any = {};

  onHiding() {
    this.visibleChange.emit(false);
  }

  onSubmit($event: SubmitEvent) {
    $event.preventDefault();
    const updateClinicDto: UpdateClinicDto = {
      id: this.selectedClinic.id,
      name: this.updateClinicFormData.name,
      location: this.updateClinicFormData.location,
      sendgridApiKey: this.updateClinicFormData.sendgridApiKey,
      email: this.updateClinicFormData.email
    };
    this.updateClinic.emit(updateClinicDto);
  }
}
